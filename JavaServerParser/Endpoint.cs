using System.Collections.Generic;
using System.Linq;

namespace JavaServerParser
{
    public class Endpoint
    {
        private readonly List<Variable> _requestParameters;

        public Endpoint(string url, EndpointType endpointType, string name, string returnType,
            List<Variable> requestedParameters,
            Variable? requestedBody)
        {
            URL = url;
            EndpointType = endpointType;
            ReturnType = returnType;
            Name = name;
            _requestParameters = new List<Variable>(requestedParameters);
            RequestBody = requestedBody;
        }

        public string URL { get; }
        public EndpointType EndpointType { get; }
        public string Name { get; }
        public string ReturnType { get; }
        public IReadOnlyList<Variable> RequestParameters => _requestParameters;
        public Variable? RequestBody { get; }

        public override string ToString()
        {
            string parameters = "";
            RequestParameters.ToList().ForEach(p => parameters += p.ToString() + "\n");

            return $"Name {Name}\n" +
                   $"ReturnType {ReturnType}\n" +
                   "URL " + URL + "\n" +
                   $"EndpointType {EndpointType}\n" +
                   $"RequestBody {RequestBody}\n" +
                   "RequestParameters\n" + parameters;
        }
    }

    public enum EndpointType
    {
        Get = 1,
        Post = 2,
    }
}