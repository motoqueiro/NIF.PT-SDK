namespace NIF.PT.Client
{
    using Flurl;
    using Flurl.Http;
    using Flurl.Http.Configuration;
    using Newtonsoft.Json;
    using NIF.PT.Client.Converters;
    using NIF.PT.Client.Entities;
    using System;
    using System.Threading.Tasks;

    public class NifClient
    {
        public const string BaseAddress = "http://www.nif.pt/";

        private string _key;

        static NifClient()
        {
            FlurlHttp.Configure(gfhs =>
            {
                var settings = new JsonSerializerSettings();
                settings.Converters.Add(new RecordJsonConverter());
                gfhs.JsonSerializer = new NewtonsoftJsonSerializer(settings);
            });
        }

        private string Url
        {
            get
            {
                if (!string.IsNullOrEmpty(_key))
                {
                    return BaseAddress.SetQueryParam("key", _key);
                }

                return BaseAddress;
            }
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="key"></param>
        public NifClient(string key = null) => this._key = key;

        /// <summary>
        /// Pesquisa informações por NIF.
        /// </summary>
        /// <param name="nif">NIF a pesquisar</param>
        /// <returns></returns>
        public async Task<SearchResponse> SearchAsync(string nif) => await Url
            .SetQueryParam("json", 1)
            .SetQueryParam("q", nif)
            .GetJsonAsync<SearchResponse>();

        /// <summary>
        /// Se ultrapassar os limites de utilização gratuita, poderá carregar a sua conta com créditos. Usando um pedido como exemplo abaixo, obterá os dados para pagamento, que poderá usar em qualquer caixa multibanco ou no seu homebanking. Os parâmetros invoice_name e invoice_nif não são obrigatórios (nestes casos, a fatura será emitida a "Consumidor Final"), mas se invoice_nif for enviado, tem de ser um NIF válido.
        /// </summary>
        /// <param name="creditsAmount">Número de créditos a comprar</param>
        /// <param name="invoiceName">Nome para faturação</param>
        /// <param name="invoiceNif">NIF para faturação</param>
        /// <returns></returns>
        public async Task<CreditPurchaseResponse> BuyCreditsAsync(
            uint creditsAmount,
            string invoiceName = null,
            string invoiceNif = null)
        {
            if (string.IsNullOrWhiteSpace(_key))
            {
                throw new ArgumentNullException("A chave da API é necessária para compra de créditos!", "key");
            }

            if (creditsAmount == 0)
            {
                throw new ArgumentException("O número de créditos a comprar não pode ser zero!", nameof(creditsAmount));
            }

            var url = Url
                .SetQueryParam("json", 1)
                .SetQueryParam("buy", creditsAmount);
            if (!string.IsNullOrEmpty(invoiceName))
            {
                url.SetQueryParam("invoice_name", invoiceName);
            }

            if (!string.IsNullOrEmpty(invoiceNif))
            {
                url.SetQueryParam("invoice_nif", invoiceNif);
            }

            return await url.GetJsonAsync<CreditPurchaseResponse>();
        }

        /// <summary>
        /// Para saber quantos créditos já gastou, sejam eles gratuitos ou pagos.
        /// </summary>
        /// <returns></returns>
        public async Task<CreditVerificationResponse> VerifyCreditsAsync()
        {
            if (string.IsNullOrWhiteSpace(_key))
            {
                throw new ArgumentNullException("A chave da API é necessária para verificação de créditos!", "key");
            }

            return await Url
                .SetQueryParam("json", 1)
                .SetQueryParam("credits", 1)
                .GetJsonAsync<CreditVerificationResponse>();
        }
    }
}