namespace Agilize___Transferência;

public class Company
{
    public Company(int number, string xpath)
    {
        Number = number;
        Xpath = xpath;
    }

    public int Number { get; }
    public string Xpath { get; }

    public static List<Company> GetCompaniesList()
    {
        var companies = new Dictionary<int, string>
        {
            { 1, "//*[@id=\"sel_empresa_portal_usuario\"]/option[24]" },
            { 3, "//*[@id=\"sel_empresa_portal_usuario\"]/option[28]" },
            { 4, "//*[@id=\"sel_empresa_portal_usuario\"]/option[16]" },
            { 6, "//*[@id=\"sel_empresa_portal_usuario\"]/option[29]" },
            { 7, "//*[@id=\"sel_empresa_portal_usuario\"]/option[15]" },
            { 8, "//*[@id=\"sel_empresa_portal_usuario\"]/option[26]" },
            { 9, "//*[@id=\"sel_empresa_portal_usuario\"]/option[17]" },
            { 10, "//*[@id=\"sel_empresa_portal_usuario\"]/option[9]" },
            { 11, "//*[@id=\"sel_empresa_portal_usuario\"]/option[4]" },
            { 12, "//*[@id=\"sel_empresa_portal_usuario\"]/option[12]" },
            { 15, "//*[@id=\"sel_empresa_portal_usuario\"]/option[27]" },
            { 16, "//*[@id=\"sel_empresa_portal_usuario\"]/option[32]" },
            { 17, "//*[@id=\"sel_empresa_portal_usuario\"]/option[8]" },
            { 18, "//*[@id=\"sel_empresa_portal_usuario\"]/option[38]" },
            { 19, "//*[@id=\"sel_empresa_portal_usuario\"]/option[30]" },
            { 20, "//*[@id=\"sel_empresa_portal_usuario\"]/option[25]" },
            { 21, "//*[@id=\"sel_empresa_portal_usuario\"]/option[7]" },
            { 22, "//*[@id=\"sel_empresa_portal_usuario\"]/option[22]" },
            { 23, "//*[@id=\"sel_empresa_portal_usuario\"]/option[19]" },
            { 24, "//*[@id=\"sel_empresa_portal_usuario\"]/option[5]" },
            { 25, "//*[@id=\"sel_empresa_portal_usuario\"]/option[42]" },
            { 26, "//*[@id=\"sel_empresa_portal_usuario\"]/option[23]" },
            { 27, "//*[@id=\"sel_empresa_portal_usuario\"]/option[20]" },
            { 28, "//*[@id=\"sel_empresa_portal_usuario\"]/option[36]" },
            { 29, "//*[@id=\"sel_empresa_portal_usuario\"]/option[21]" },
            { 30, "//*[@id=\"sel_empresa_portal_usuario\"]/option[47]" },
            { 31, "//*[@id=\"sel_empresa_portal_usuario\"]/option[6]" },
            { 32, "//*[@id=\"sel_empresa_portal_usuario\"]/option[10]" },
            { 33, "//*[@id=\"sel_empresa_portal_usuario\"]/option[13]" },
            { 34, "//*[@id=\"sel_empresa_portal_usuario\"]/option[18]" },
            { 35, "//*[@id=\"sel_empresa_portal_usuario\"]/option[39]" },
            { 36, "//*[@id=\"sel_empresa_portal_usuario\"]/option[40]" },
            { 37, "//*[@id=\"sel_empresa_portal_usuario\"]/option[11]" },
            { 38, "//*[@id=\"sel_empresa_portal_usuario\"]/option[3]" },
            { 39, "//*[@id=\"sel_empresa_portal_usuario\"]/option[44]" },
            { 40, "//*[@id=\"sel_empresa_portal_usuario\"]/option[31]" },
            { 41, "//*[@id=\"sel_empresa_portal_usuario\"]/option[33]" },
            { 42, "//*[@id=\"sel_empresa_portal_usuario\"]/option[34]" },
            { 43, "//*[@id=\"sel_empresa_portal_usuario\"]/option[35]" },
            { 44, "//*[@id=\"sel_empresa_portal_usuario\"]/option[37]" },
            { 45, "//*[@id=\"sel_empresa_portal_usuario\"]/option[41]" },
            { 46, "//*[@id=\"sel_empresa_portal_usuario\"]/option[43]" },
            { 47, "//*[@id=\"sel_empresa_portal_usuario\"]/option[46]" },
            { 50, "//*[@id=\"sel_empresa_portal_usuario\"]/option[45]" }
        };

        var list = new List<Company>(companies.Count);
        foreach (var (key, value) in companies) list.Add(new Company(key, value));

        return list;
    }

    public override string ToString()
    {
        return "Franquia " + Number;
    }
}