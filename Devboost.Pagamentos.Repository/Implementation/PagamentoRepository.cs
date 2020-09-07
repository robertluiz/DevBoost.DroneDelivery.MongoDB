using Devboost.Pagamentos.Domain.Entities;
using Devboost.Pagamentos.Domain.Interfaces.Repository;
using Devboost.Pagamentos.Repository.Model;
using ServiceStack;
using ServiceStack.OrmLite;
using System;
using System.Collections.Generic;
using System.Data;
using System.Dynamic;
using System.Linq;
using System.Threading.Tasks;

namespace Devboost.Pagamentos.Repository.Implementation
{
	public class PagamentoRepository : BaseRepository<PagamentoEntity, Pagamento>,IPagamentoRepository
	{
		private readonly IDbConnection _connection;

		public PagamentoRepository(IDbConnection connection) : base(connection)
		{
			_connection = connection;
		}
		public async Task Inserir(PagamentoEntity pagamento)
		{
			try
			{
				var model = pagamento.ConvertTo<Pagamento>();

				_connection.CreateTableIfNotExists<Cartao>();
				_connection.CreateTableIfNotExists<FormaPagamento>();
				_connection.CreateTableIfNotExists<Pagamento>();
				
				_connection.Save(model.FormaPagamento.Cartao);
				_connection.Save(model.FormaPagamento);

				await _connection.SaveAsync(model);
			}
			catch (Exception ex)
			{
				throw new Exception(ex.Message);
			}
		}

		public Task<PagamentoEntity> RetonarPagamento(Guid idPagamento)
		{
			var result = _connection.From<Pagamento>()
				.Join<Pagamento, FormaPagamento>((pt, fp) => pt.FormaPagamentoID == fp.Id)
				.Join<FormaPagamento, Cartao>((fp, ct) => fp.CartaoID == ct.Id)
				.Where(c => c.Id == idPagamento)
				.Select<Pagamento, FormaPagamento, Cartao>((p, f, c) =>
				new
				{
					idPagamento = p.Id,
					p.IdPedido,
					p.Valor,
					p.StatusPagamento,
					p.FormaPagamentoID,
					IdFormaPagamento = f.Id,
					f.CartaoID,
					idCartao = c.Id,
					c.DataValidade,
					c.NumeroCartao,
					c.Tipo,
					c.CodSeguranca,
					c.Bandeira
				});

			var QueryPagamento = _connection.Select<dynamic>(result);
			Pagamento pag = null;
			if (QueryPagamento.Count > 0)
			{
				pag = AtribuirClasse(QueryPagamento[0]);
				return Task.FromResult(pag.ConvertTo<PagamentoEntity>());
			}
			else
			{
				return null;
			}
		}

		public async Task<PagamentoEntity> GetPagamentoByIdPedido(Guid idPedido)
		{
			_connection.CreateTableIfNotExists<Pagamento>();
			var u = await _connection.SingleAsync<Pagamento>(
				c =>
					c.IdPedido == idPedido);


			return u.ConvertTo<PagamentoEntity>();
		}

		public async Task Atualizar(PagamentoEntity pagamento)
		{
			var model = pagamento.ConvertTo<Pagamento>();
			await _connection.UpdateAsync(model);
		}

		private Pagamento AtribuirClasse(dynamic pagamentos)
		{
			List<Dictionary<string, string>> propriedades = RetornarPropriedadesDinamicas(pagamentos);
			Pagamento retorno = new Pagamento();
			retorno.Id = AtribuirValor<Guid>(propriedades, "idPagamento");
			retorno.FormaPagamentoID = AtribuirValor<Guid>(propriedades, "FormaPagamentoID");
			retorno.IdPedido = AtribuirValor<Guid>(propriedades, "IdPedido");
			retorno.Valor = AtribuirValor<float>(propriedades, "Valor");
			retorno.StatusPagamento = AtribuirValor<Domain.Enums.StatusPagamentoEnum>(propriedades, "StatusPagamento");

			retorno.FormaPagamento = new FormaPagamento();
			retorno.FormaPagamento.Id = AtribuirValor<Guid>(propriedades, "IdFormaPagamento");
			retorno.FormaPagamento.CartaoID = AtribuirValor<Guid>(propriedades, "idCartao");

			retorno.FormaPagamento.Cartao = new Cartao();
			retorno.FormaPagamento.Cartao.Id = AtribuirValor<Guid>(propriedades, "idCartao");
			retorno.FormaPagamento.Cartao.Bandeira = AtribuirValor<Domain.Enums.PagamentoBandeiraEnum>(propriedades, "Bandeira");
			DateTime data = AtribuirValor<DateTime>(propriedades, "DataValidade");
			retorno.FormaPagamento.Cartao.DataValidade = new DateTime(data.Year, data.Day, data.Month, data.Hour, data.Minute, data.Second);
			retorno.FormaPagamento.Cartao.CodSeguranca = AtribuirValor<string>(propriedades, "CodSeguranca");
			retorno.FormaPagamento.Cartao.NumeroCartao = AtribuirValor<string>(propriedades, "NumeroCartao");
			retorno.FormaPagamento.Cartao.Tipo = AtribuirValor<Domain.Enums.TipoCartaoEnum>(propriedades, "Tipo");
			
			return retorno;
		}
		private List<Dictionary<string, string>> RetornarPropriedadesDinamicas(dynamic objetoDinamico)
		{
			try
			{
				ExpandoObject attributesAsJObject = objetoDinamico;
				var values = (IDictionary<string, object>)attributesAsJObject;
				List<Dictionary<string, string>> toReturn = new List<Dictionary<string, string>>();
				foreach (var item in values)
				{
					Dictionary<string, string> propriedade = new Dictionary<string, string>();
					propriedade.Add(item.Key, item.Value.ToString());
					toReturn.Add(propriedade);
				}
				return toReturn;
			}
			catch (Exception ex)
			{
				throw new Exception(ex.Message);
			}
		}
		private TipoRetorno AtribuirValor<TipoRetorno>(List<Dictionary<string, string>> listaPropriedade, string nomePropriedade)
		{
			string valor = string.Empty;
			listaPropriedade.FirstOrDefault(c => c.ContainsKey(nomePropriedade)).TryGetValue(nomePropriedade, out valor);
			TipoRetorno retorno = valor.ConvertTo<TipoRetorno>();
			return retorno;
		}
	}

}