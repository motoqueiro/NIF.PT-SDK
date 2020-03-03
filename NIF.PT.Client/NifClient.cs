namespace NIF.PT.Client
{
    using System;
    using System.Threading.Tasks;
    using Flurl;
    using Flurl.Http;
    using Flurl.Http.Configuration;
    using Newtonsoft.Json;
    using NIF.PT.Client.Converters;
    using NIF.PT.Client.Entities;

    public class NifClient
    {
        public const string BaseAddress = "http://www.nif.pt/";

        public string Key { get; private set; }

        static NifClient()
        {
            FlurlHttp.Configure(gfhs =>
            {
                var settings = new JsonSerializerSettings();
                settings.Converters.Add(new RecordJsonConverter());
                gfhs.JsonSerializer = new NewtonsoftJsonSerializer(settings);
            });
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="key"></param>
        public NifClient(string key)
        {
            if (string.IsNullOrEmpty(key))
            {
                throw new ArgumentNullException(nameof(key));
            }

            this.Key = key;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="nif"></param>
        /// <returns></returns>
        public async Task<SearchResponse> Search(string nif) => await BaseAddress
            .SetQueryParam("json", 1)
            .SetQueryParam("q", nif)
            .SetQueryParam("key", this.Key)
            .GetJsonAsync<SearchResponse>();

        /// <summary>
        /// Se ultrapassar os limites de utilização gratuita, poderá carregar a sua conta com créditos. Usando um pedido como exemplo abaixo, obterá os dados para pagamento, que poderá usar em qualquer caixa multibanco ou no seu homebanking. Os parâmetros invoice_name e invoice_nif não são obrigatórios (nestes casos, a fatura será emitida a "Consumidor Final"), mas se invoice_nif for enviado, tem de ser um NIF válido.
        /// </summary>
        /// <param name="creditsAmount">Número de créditos a comprar</param>
        /// <param name="invoiceName">Nome para faturação</param>
        /// <param name="invoiceNif">NIF para faturação</param>
        /// <returns></returns>
        public async Task<CreditPurchaseResponse> BuyCredits(
            uint creditsAmount,
            string invoiceName = null,
            string invoiceNif = null)
        {
            if (creditsAmount == 0)
            {
                throw new ArgumentException("The number of credits to buy cannot be zero!", nameof(creditsAmount));
            }

            var url = BaseAddress
                .SetQueryParam("json", 1)
                .SetQueryParam("buy", creditsAmount)
                .SetQueryParam("key", this.Key);
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
        public async Task<CreditVerificationResponse> VerifyCredits() => await BaseAddress
            .SetQueryParam("json", 1)
            .SetQueryParam("credits", 1)
            .SetQueryParam("key", this.Key)
            .GetJsonAsync<CreditVerificationResponse>();
    }
}