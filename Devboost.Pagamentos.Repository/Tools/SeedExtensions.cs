using System.Data;
using System.Threading.Tasks;
using Devboost.Pagamentos.Repository.Model;
using ServiceStack.OrmLite;

namespace Devboost.Pagamentos.Repository.Tools
{
    public static class SeedExtensions
    {

        public static void CheckBase(this IDbConnection con)
        {
            
            con.CreateTableIfNotExists<Cartao>();
            con.CreateTableIfNotExists<Boleto>();
            //con.CreateTableIfNotExists<FormaPagamento>();
            con.CreateTableIfNotExists<Pagamento>();

        }
    }
}