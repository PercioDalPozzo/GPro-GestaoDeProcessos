namespace API.Controllers
{
    public class RetornoViewModel
    {
        public object Content { get; private set; }
        public bool Sucesso { get; private set; }
        public string Msg { get; private set; }

        internal static RetornoViewModel RetornoErro(string message)
        {
            return new RetornoViewModel
            {
                Msg = message,
                Sucesso = false
            };
        }

        internal static RetornoViewModel RetornoSucesso()
        {
            var ret = new RetornoViewModel
            {
                Sucesso = true
            };

            return ret;
        }

        internal static RetornoViewModel RetornoSucesso(object content)
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
