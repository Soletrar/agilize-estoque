namespace Agilize___Transferência;

public class Stock
{
    public Stock(string name, string xPath)
    {
        Name = name;
        XPath = xPath;
    }

    public string Name { get; }
    public string XPath { get; }

    public static List<Stock> GetStockList()
    {
        var stocks = new Dictionary<string, string>
        {
            { "ESTOQUE VENDAS", "/html/body/table/tbody/tr[2]/td/form/table/tbody/tr[2]/td[3]/select/option[1]" },
            { "DEPOSITO EXTRA", "/html/body/table/tbody/tr[2]/td/form/table/tbody/tr[2]/td[3]/select/option[6]" }
        };

        var list = new List<Stock>(stocks.Count);
        foreach (var (key, value) in stocks) list.Add(new Stock(key, value));

        return list;
    }

    public override string ToString()
    {
        return Name;
    }
}