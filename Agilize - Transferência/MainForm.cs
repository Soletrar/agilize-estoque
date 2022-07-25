using Agilize___Transferência.Models;
using Newtonsoft.Json;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace Agilize___Transferência;

public partial class MainForm : Form
{
    private readonly List<string> _productsNotFound = new();

    private List<Company> _companies;

    private Config _config;

    private List<Stock> _stocks;

    public MainForm()
    {
        InitializeComponent();

        if (!File.Exists("config.cfg")) WriteDefaultConfig();

        LoadConfig();
    }

    private void WriteDefaultConfig()
    {
        var config = new Config
        {
            Name = "",
            Login = "",
            Password = "",
            ProductsLink = "https://linx.microvix.com.br:8371/gestor_web/produtos/cadastro_produtos.asp"
        };

        var json = JsonConvert.SerializeObject(config, Formatting.Indented);
        File.WriteAllText("config.cfg", json);
    }

    private void LoadConfig()
    {
        _config = JsonConvert.DeserializeObject<Config>(File.ReadAllText("config.cfg"));
    }

    private void Form1_Load(object sender, EventArgs e)
    {
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

        // adição de estoque
        cbboxAdicaoFranchise.Items.AddRange(_companies.ToArray());
        cbboxAdicaoFranchise.SelectedIndex = random.Next(cbboxAdicaoFranchise.Items.Count);

        cbboxAdicaoStock.Items.AddRange(_stocks.ToArray());
        cbboxAdicaoStock.SelectedIndex = random.Next(cbboxAdicaoStock.Items.Count);
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

            var tasks = new Task[2];
            tasks[0] = Task.Run(() => FazTransferenciaFranquia(false, from.Xpath, produtosDictionary));
            tasks[1] = Task.Run(() => FazTransferenciaFranquia(true, to.Xpath, produtosDictionary));

            await Task.WhenAll(tasks);

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

            var tasks = new Task[2];
            tasks[0] = Task.Run(() => FazTransferenciaDeposito(false, franchise.Xpath, from.XPath, produtosDictionary));
            tasks[1] = Task.Run(() => FazTransferenciaDeposito(true, franchise.Xpath, to.XPath, produtosDictionary));

            await Task.WhenAll(tasks);

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

    private async void btnStartAdicao_Click(object sender, EventArgs e)
    {
        var franchise = cbboxAdicaoFranchise.SelectedItem as Company;
        var stock = cbboxAdicaoStock.SelectedItem as Stock;


        if (MessageBox.Show($"Você irá adicionar produtos na {franchise} no {stock}. \n\nDeseja continuar?",
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

            await FazTransferenciaDeposito(true, franchise.Xpath, stock.XPath, produtosDictionary);

            TopMost = true;

            if (_productsNotFound.Count > 0)
            {
                await File.WriteAllLinesAsync("codigos.txt", _productsNotFound);

                MessageBox.Show(
                    "Um ou mais produtos não foram alterados. Abra o arquivo codigos.txt para uma lista com os códigos de barras não encontrados.");
            }

            MessageBox.Show("Adição realizada!");

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

        var chromeDriver = new ChromeDriver();
        chromeDriver.Manage().Window.Maximize();

        chromeDriver.Url = "https://erp.linx.com.br/";

        chromeDriver.FindElement(By.Id("f_login")).SendKeys(_config.Login);
        chromeDriver.FindElement(By.Id("f_senha")).SendKeys(_config.Password);
        chromeDriver.FindElement(By.XPath("//*[@id=\"form_login\"]/button[1]")).Click();

        while (true)
            try
            {
                // selecionar empresa
                chromeDriver.FindElement(By.XPath(companyXpath)).Click();
                chromeDriver.FindElement(By.XPath("//*[@id=\"btnselecionar_empresa\"]")).Click();
                break;
            }
            catch (Exception)
            {
                // ignore
            }

        foreach (var (codigoBarras, quantidade) in codigoBarrasList)
        {
            var quantidadeEditavel = quantidade;


            chromeDriver.Url = _config.ProductsLink;

            while (true)
                try
                {
                    chromeDriver.FindElement(By.Id("f_conteudo"))
                        .SendKeys(codigoBarras); // codigo de barras do produto
                    chromeDriver.FindElement(By.XPath("//*[@id=\"campo4\"]")).Click(); // selecionar codigo de barras
                    chromeDriver
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
                    chromeDriver.FindElement(By.XPath("//*[@id=\"AutoNumber4\"]/tbody/tr[3]/td[7]/div[3]"))
                        .Click(); // botão alterar saldo
                    break;
                }
                catch (Exception)
                {
                    if (chromeDriver.PageSource.Contains(
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
                        int.Parse(chromeDriver.FindElement(By.Name("velho_saldo"))
                            .GetAttribute("value")); // obtem o saldo atual

                    if (!add)
                    {
                        if (quantidadeAtual <= 0) break;
                        if (quantidadeAtual - quantidade < 0) quantidadeEditavel = quantidadeAtual;
                    }

                    chromeDriver.FindElement(By.Name("novo_saldo")).Clear(); // limpa o campo de novo saldo

                    chromeDriver.FindElement(By.Name("novo_saldo"))
                        .SendKeys(add
                            ? quantidadeAtual + quantidade + "00"
                            : quantidadeAtual - quantidadeEditavel + "00");

                    chromeDriver.FindElement(By.Name("motivo_ajuste"))
                        .SendKeys($"Solicitado por e-mail - {_config.Name}"); // motivo do ajuste
                    chromeDriver
                        .FindElement(
                            By.XPath("/html/body/table/tbody/tr[2]/td/form/table/tbody/tr[6]/td[2]/font/input"))
                        .Click(); // Ajustar Quantidade


                    while (true)
                    {
                        if (chromeDriver.PageSource.Contains("Confirmação de ajuste de saldo de produtos")) break;

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

        chromeDriver.Quit();
    }

    private async Task FazTransferenciaDeposito(bool add, string companyXpath, string stockXpath,
        Dictionary<string, int> codigoBarrasList)
    {
        _productsNotFound.Clear();

        var chromeDriver = new ChromeDriver();
        chromeDriver.Manage().Window.Maximize();

        chromeDriver.Url = "https://erp.linx.com.br/";

        chromeDriver.FindElement(By.Id("f_login")).SendKeys(_config.Login);
        chromeDriver.FindElement(By.Id("f_senha")).SendKeys(_config.Password);
        chromeDriver.FindElement(By.XPath("//*[@id=\"form_login\"]/button[1]")).Click();

        while (true)
            try
            {
                // selecionar empresa
                chromeDriver.FindElement(By.XPath(companyXpath)).Click();
                chromeDriver.FindElement(By.XPath("//*[@id=\"btnselecionar_empresa\"]")).Click();
                break;
            }
            catch (Exception)
            {
                // ignore
            }

        foreach (var (codigoBarras, quantidade) in codigoBarrasList)
        {
            var quantidadeEditavel = quantidade;

            chromeDriver.Url = _config.ProductsLink;

            while (true)
                try
                {
                    chromeDriver.FindElement(By.Id("f_conteudo"))
                        .SendKeys(codigoBarras); // codigo de barras do produto
                    chromeDriver.FindElement(By.XPath("//*[@id=\"campo4\"]")).Click(); // selecionar codigo de barras
                    chromeDriver
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
                    chromeDriver.FindElement(By.XPath("//*[@id=\"AutoNumber4\"]/tbody/tr[3]/td[7]/div[3]"))
                        .Click(); // botão alterar saldo
                    break;
                }
                catch (Exception)
                {
                    if (chromeDriver.PageSource.Contains(
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
                    chromeDriver.FindElement(By.XPath(stockXpath)).Click();

                    await Task.Delay(300);

                    var quantidadeAtual =
                        int.Parse(chromeDriver.FindElement(By.Name("velho_saldo"))
                            .GetAttribute("value")); // obtem o saldo atual

                    if (!add)
                    {
                        if (quantidadeAtual <= 0) break;
                        if (quantidadeAtual - quantidade < 0) quantidadeEditavel = quantidadeAtual;
                    }

                    chromeDriver.FindElement(By.Name("novo_saldo")).Clear(); // limpa o campo de novo saldo

                    chromeDriver.FindElement(By.Name("novo_saldo"))
                        .SendKeys(add
                            ? quantidadeAtual + quantidade + "00"
                            : quantidadeAtual - quantidadeEditavel + "00");

                    chromeDriver.FindElement(By.Name("motivo_ajuste"))
                        .SendKeys($"Solicitado por e-mail - {_config.Name}"); // motivo do ajuste
                    chromeDriver
                        .FindElement(
                            By.XPath("/html/body/table/tbody/tr[2]/td/form/table/tbody/tr[6]/td[2]/font/input"))
                        .Click(); // Ajustar Quantidade


                    while (true)
                    {
                        if (chromeDriver.PageSource.Contains("Confirmação de ajuste de saldo de produtos")) break;

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

        chromeDriver.Quit();
    }
}