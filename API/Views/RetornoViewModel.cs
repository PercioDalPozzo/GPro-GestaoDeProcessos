namespace API.Controllers
{
    public class RetornoViewModel
    {
        public object Content { get; set; }
        public bool Sucesso { get; set; }
        public string Msg { get; set; }

        public static RetornoViewModel RetornoErro(string message)
        {
            return new RetornoViewModel
            {
                Msg = message,
                Sucesso = false
            };
        }

        public static RetornoViewModel RetornoSucesso()
        {
            var ret = new RetornoViewModel
            {
                Sucesso = true
            };

            return ret;
        }

        public static RetornoViewModel RetornoSucesso(object content)
        {
            var ret = new RetornoViewModel
            {
                Content = content,
                Sucesso = true
            };

            return ret;
        }
    }
}
