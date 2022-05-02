namespace Worter
{
    public class Request
    {
        public string Method { get; set; }
        public string Url { get; set; }
        public object Parameter { get; set; }
        public bool CheckAuth { get; set; } = true;

        private Request(string method, string url, object parameter, bool checkAuth)
        {
            this.Method = method;
            this.Url = url;
            this.Parameter = parameter;
            this.CheckAuth = checkAuth;
        }

        public static Request BuildGet(string url, object parameter = null, bool checkAuth = true)
        {
            return new Request("GET", url, parameter, checkAuth);
        }

        public static Request BuildPost(string url, object parameter = null, bool checkAuth = true)
        {
            return new Request("POST", url, parameter, checkAuth);
        }

        public static Request BuildDelete(string url, object parameter = null, bool checkAuth = true)
        {
            return new Request("DELETE", url, parameter, checkAuth);
        }
    }
}
