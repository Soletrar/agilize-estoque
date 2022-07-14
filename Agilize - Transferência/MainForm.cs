using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace Agilize___Transferência;

public partial class MainForm : Form
{
    private readonly List<string> _productsNotFound = new();

    private ChromeDriver _chromeDriver;

    private List<Company> _companies;

    private string _name;
    private string _password;
    private List<Stock> _stocks;
    private string _user;

    public MainForm()
    {
        InitializeComponent();
    }

    private void Form1_Load(object sender, EventArgs e)
    {
        LoadUserData();

        LoadControls();
    }

    private void LoadControls()
    {
        _companies = Company.GetCompaniesList();

        var random = new Random();

        // transferencia de franquias
        cbboxFrom.Items.AddRange(_companies.ToArray());
        cbboxFrom.SelectedIndex = random.Next(cbboxFrom.Items.Count);

        cbboxTo.Items.AddRange(_companies.ToArray());
        cbboxTo.SelectedIndex = random.Next(cbboxTo.Items.Count);

        cbboxFranchiseStock.Items.AddRange(_companies.ToArray());
        cbboxFranchiseStock.SelectedIndex = random.Next(cbboxFranchiseStock.Items.Count);


        // transferencia de estoque
        _stocks = Stock.GetStockList();

        cbboxFromStock.Items.AddRange(_stocks.ToArray());
        cbboxFromStock.SelectedIndex = random.Next(cbboxFromStock.Items.Count);

        cbboxToStock.Items.AddRange(_stocks.ToArray());
        cbboxToStock.SelectedIndex = random.Next(cbboxToStock.Items.Count);


        // baixa de estoque
        cbboxBaixaFranchise.Items.AddRange(_companies.ToArray());
        cbboxBaixaFranchise.SelectedIndex = random.Next(cbboxBaixaFranchise.Items.Count);

        cbboxBaixaStock.Items.AddRange(_stocks.ToArray());
        cbboxBaixaStock.SelectedIndex = random.Next(cbboxBaixaStock.Items.Count);
    }

    private void LoadUserData()
    {
        var data = File.ReadAllText("dados.txt").Trim();

        var split = data.Split(';');
        _name = split[0];
        _user = split[1];
        _password = split[2];
    }

    private void DisableControls()
    {
        Enabled = false;
    }

    private void EnableControls()
    {
        Enabled = true;
    }

    private async void btnStart_Click(object sender, EventArgs e)
    {
        var from = cbboxFrom.SelectedItem as Company;
        var to = cbboxTo.SelectedItem as Company;


        if (MessageBox.Show(
                $"Você irá iniciar uma transferência de franquia de {from} para {to}. \n\nDeseja continuar?", "Atenção",
                MessageBoxButtons.YesNo) == DialogResult.Yes)
        {
            DisableControls();

            var produtosDictionary = await LoadProductsAsync();

            if (produtosDictionary.Count == 0)
            {
                btnStart.Enabled = cbboxFrom.Enabled = cbboxTo.Enabled = true;
                MessageBox.Show("Você deve especificar os produtos e suas quantidades!");
                return;
            }

            await FazTransferenciaFranquia(false, from.Xpath, produtosDictionary);
            await FazTransferenciaFranquia(true, to.Xpath, produtosDictionary);


            TopMost = true;

            if (_productsNotFound.Count > 0)
            {
                await File.WriteAllLinesAsync("codigos.txt", _productsNotFound);

                MessageBox.Show(
                    "Um ou mais produtos não foram alterados. Abra o arquivo codigos.txt para uma lista com os códigos de barras não encontrados.");
            }

            MessageBox.Show("Transferência realizada!");

            TopMost = false;

            EnableControls();
        }
    }

    private async void btnStartStock_Click(object sender, EventArgs e)
    {
        var franchise = cbboxFranchiseStock.SelectedItem as Company;
        var from = cbboxFromStock.SelectedItem as Stock;
        var to = cbboxToStock.SelectedItem as Stock;


        if (MessageBox.Show($"Você irá iniciar uma transferência de estoque de {from} para {to}. \n\nDeseja continuar?",
                "Atenção",
                MessageBoxButtons.YesNo) == DialogResult.Yes)
        {
            DisableControls();

            var produtosDictionary = await LoadProductsAsync();

            if (produtosDictionary.Count == 0)
            {
                btnStart.Enabled = cbboxFrom.Enabled = cbboxTo.Enabled = true;
                MessageBox.Show("Você deve especificar os produtos e suas quantidades!");
                return;
            }

            await FazTransferenciaDeposito(false, franchise.Xpath, from.XPath, produtosDictionary);
            await FazTransferenciaDeposito(true, franchise.Xpath, to.XPath, produtosDictionary);

            TopMost = true;

            if (_productsNotFound.Count > 0)
            {
                await File.WriteAllLinesAsync("codigos.txt", _productsNotFound);

                MessageBox.Show(
                    "Um ou mais produtos não foram alterados. Abra o arquivo codigos.txt para uma lista com os códigos de barras não encontrados.");
            }

            MessageBox.Show("Transferência realizada!");

            TopMost = false;

            EnableControls();
        }
    }

    private async void btnStartBaixa_Click(object sender, EventArgs e)
    {
        var franchise = cbboxBaixaFranchise.SelectedItem as Company;
        var stock = cbboxBaixaStock.SelectedItem as Stock;


        if (MessageBox.Show($"Você irá dar baixa nos produtos de {franchise} no {stock}. \n\nDeseja continuar?",
                "Atenção",
                MessageBoxButtons.YesNo) == DialogResult.Yes)
        {
            DisableControls();

            var produtosDictionary = await LoadProductsAsync();

            if (produtosDictionary.Count == 0)
            {
                btnStart.Enabled = cbboxFrom.Enabled = cbboxTo.Enabled = true;
                MessageBox.Show("Você deve especificar os produtos e suas quantidades!");
                return;
            }

            await FazTransferenciaDeposito(false, franchise.Xpath, stock.XPath, produtosDictionary);

            TopMost = true;

            if (_productsNotFound.Count > 0)
            {
                await File.WriteAllLinesAsync("codigos.txt", _productsNotFound);

                MessageBox.Show(
                    "Um ou mais produtos não foram alterados. Abra o arquivo codigos.txt para uma lista com os códigos de barras não encontrados.");
            }

            MessageBox.Show("Baixa realizada!");

            TopMost = false;

            EnableControls();
        }
    }

    private static async Task<Dictionary<string, int>> LoadProductsAsync()
    {
        var dict = new Dictionary<string, int>();
        var lines = await File.ReadAllLinesAsync("produtos.txt");
        foreach (var line in lines)
        {
            if (string.IsNullOrWhiteSpace(line)) continue;

            var split = line.Split(';');
            if (split.Length == 2) dict.TryAdd(split[0].Trim(), int.Parse(split[1].Trim()));
        }

        return dict;
    }

    private async Task FazTransferenciaFranquia(bool add, string companyXpath, Dictionary<string, int> codigoBarrasList)
    {
        _productsNotFound.Clear();

        _chromeDriver = new ChromeDriver();
        _chromeDriver.Manage().Window.Maximize();

        _chromeDriver.Url = "https://erp.linx.com.br/";

        _chromeDriver.FindElement(By.Id("f_login")).SendKeys(_user);
        _chromeDriver.FindElement(By.Id("f_senha")).SendKeys(_password);
        _chromeDriver.FindElement(By.XPath("//*[@id=\"form_login\"]/button[1]")).Click();

        while (true)
            try
            {
                // selecionar empresa
                _chromeDriver.FindElement(By.XPath(companyXpath)).Click();
                _chromeDriver.FindElement(By.XPath("//*[@id=\"btnselecionar_empresa\"]")).Click();
                break;
            }
            catch (Exception)
            {
                // ignore
            }

        foreach (var (codigoBarras, quantidade) in codigoBarrasList)
        {
            var quantidadeEditavel = quantidade;


            _chromeDriver.Url = "https://linx.microvix.com.br:8370/gestor_web/produtos/cadastro_produtos.asp";

            while (true)
                try
                {
                    _chromeDriver.FindElement(By.Id("f_conteudo"))
                        .SendKeys(codigoBarras); // codigo de barras do produto
                    _chromeDriver.FindElement(By.XPath("//*[@id=\"campo4\"]")).Click(); // selecionar codigo de barras
                    _chromeDriver
                        .FindElement(By.XPath("/html/body/table/tbody/tr[4]/td[2]/form/table/tbody/tr/td[3]/input"))
                        .Click(); // Pesquisar
                    break;
                }
                catch (Exception)
                {
                    // ignore
                }


            while (true)
                try
                {
                    _chromeDriver.FindElement(By.XPath("//*[@id=\"AutoNumber4\"]/tbody/tr[3]/td[7]/div[3]"))
                        .Click(); // botão alterar saldo
                    break;
                }
                catch (Exception)
                {
                    if (_chromeDriver.PageSource.Contains(
                            "Não foram encontrados registros com os critérios de pesquisa especificados."))
                    {
                        if (!_productsNotFound.Contains(codigoBarras)) _productsNotFound.Add(codigoBarras);

                        goto End;
                    }
                }

            while (true)
                try
                {
                    var quantidadeAtual =
                        int.Parse(_chromeDriver.FindElement(By.Name("velho_saldo"))
                            .GetAttribute("value")); // obtem o saldo atual

                    if (!add)
                    {
                        if (quantidadeAtual <= 0) break;
                        if (quantidadeAtual - quantidade < 0) quantidadeEditavel = quantidadeAtual;
                    }

                    _chromeDriver.FindElement(By.Name("novo_saldo")).Clear(); // limpa o campo de novo saldo

                    _chromeDriver.FindElement(By.Name("novo_saldo"))
                        .SendKeys(add
                            ? (quantidadeAtual + quantidade).ToString()
                            : (quantidadeAtual - quantidadeEditavel).ToString());

                    _chromeDriver.FindElement(By.Name("motivo_ajuste"))
                        .SendKeys($"Solicitado por e-mail - {_name}"); // motivo do ajuste
                    _chromeDriver
                        .FindElement(
                            By.XPath("/html/body/table/tbody/tr[2]/td/form/table/tbody/tr[6]/td[2]/font/input"))
                        .Click(); // Ajustar Quantidade


                    while (true)
                    {
                        if (_chromeDriver.PageSource.Contains("Confirmação de ajuste de saldo de produtos")) break;

                        await Task.Delay(100);
                    }

                    break;
                }
                catch (Exception)
                {
                    // ignore
                }

            End: ;
        }

        _chromeDriver.Quit();
    }

    private async Task FazTransferenciaDeposito(bool add, string companyXpath, string stockXpath,
        Dictionary<string, int> codigoBarrasList)
    {
        _productsNotFound.Clear();

        _chromeDriver = new ChromeDriver();
        _chromeDriver.Manage().Window.Maximize();

        _chromeDriver.Url = "https://erp.linx.com.br/";

        _chromeDriver.FindElement(By.Id("f_login")).SendKeys(_user);
        _chromeDriver.FindElement(By.Id("f_senha")).SendKeys(_password);
        _chromeDriver.FindElement(By.XPath("//*[@id=\"form_login\"]/button[1]")).Click();

        while (true)
            try
            {
                // selecionar empresa
                _chromeDriver.FindElement(By.XPath(companyXpath)).Click();
                _chromeDriver.FindElement(By.XPath("//*[@id=\"btnselecionar_empresa\"]")).Click();
                break;
            }
            catch (Exception)
            {
                // ignore
            }

        foreach (var (codigoBarras, quantidade) in codigoBarrasList)
        {
            var quantidadeEditavel = quantidade;


            _chromeDriver.Url = "https://linx.microvix.com.br:8370/gestor_web/produtos/cadastro_produtos.asp";

            while (true)
                try
                {
                    _chromeDriver.FindElement(By.Id("f_conteudo"))
                        .SendKeys(codigoBarras); // codigo de barras do produto
                    _chromeDriver.FindElement(By.XPath("//*[@id=\"campo4\"]")).Click(); // selecionar codigo de barras
                    _chromeDriver
                        .FindElement(By.XPath("/html/body/table/tbody/tr[4]/td[2]/form/table/tbody/tr/td[3]/input"))
                        .Click(); // Pesquisar
                    break;
                }
                catch (Exception)
                {
                    // ignore
                }

            while (true)
                try
                {
                    _chromeDriver.FindElement(By.XPath("//*[@id=\"AutoNumber4\"]/tbody/tr[3]/td[7]/div[3]"))
                        .Click(); // botão alterar saldo
                    break;
                }
                catch (Exception)
                {
                    if (_chromeDriver.PageSource.Contains(
                            "Não foram encontrados registros com os critérios de pesquisa especificados."))
                    {
                        if (!_productsNotFound.Contains(codigoBarras)) _productsNotFound.Add(codigoBarras);
                        goto End;
                    }
                }

            while (true)
                try
                {
                    // seleciona deposito
                    _chromeDriver.FindElement(By.XPath(stockXpath)).Click();

                    await Task.Delay(300);

                    var quantidadeAtual =
                        int.Parse(_chromeDriver.FindElement(By.Name("velho_saldo"))
                            .GetAttribute("value")); // obtem o saldo atual

                    if (!add)
                    {
                        if (quantidadeAtual <= 0) break;
                        if (quantidadeAtual - quantidade < 0) quantidadeEditavel = quantidadeAtual;
                    }

                    _chromeDriver.FindElement(By.Name("novo_saldo")).Clear(); // limpa o campo de novo saldo

                    _chromeDriver.FindElement(By.Name("novo_saldo"))
                        .SendKeys(add
                            ? (quantidadeAtual + quantidade).ToString()
                            : (quantidadeAtual - quantidadeEditavel).ToString());

                    _chromeDriver.FindElement(By.Name("motivo_ajuste"))
                        .SendKeys($"Solicitado por e-mail - {_name}"); // motivo do ajuste
                    _chromeDriver
                        .FindElement(
                            By.XPath("/html/body/table/tbody/tr[2]/td/form/table/tbody/tr[6]/td[2]/font/input"))
                        .Click(); // Ajustar Quantidade


                    while (true)
                    {
                        if (_chromeDriver.PageSource.Contains("Confirmação de ajuste de saldo de produtos")) break;

                        await Task.Delay(100);
                    }

                    break;
                }
                catch (Exception)
                {
                    // ignore
                }

            End: ;
        }

        _chromeDriver.Quit();
    }
}