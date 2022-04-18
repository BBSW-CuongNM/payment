namespace Ultils.Extensions;

public static class HttpClientExtensions
{
    public static Task<T> ExecuteGet<T>(
        this HttpClient httpClient,
        string url,
        IDictionary<string, string> headers = null,
        CancellationToken cancellationToken = default)
    {
        return httpClient.Execute<T>(
            HttpMethod.Get,
            url,
            headers: headers,
            cancellationToken: cancellationToken);
    }

    public static Task<T> ExecutePost<T>(
        this HttpClient httpClient,
        string url,
        object data = null,
        IDictionary<string, string> headers = null,
        CancellationToken cancellationToken = default)
    {
        return httpClient.Execute<T>(
            HttpMethod.Post,
            url,
            data: data,
            headers: headers,
            cancellationToken: cancellationToken);
    }

    public static Task<T> ExecutePut<T>(
        this HttpClient httpClient,
        string url,
        object data = null,
        IDictionary<string, string> headers = null,
        CancellationToken cancellationToken = default)
    {
        return httpClient.Execute<T>(
            HttpMethod.Put,
            url,
            data: data,
            headers: headers,
            cancellationToken: cancellationToken);
    }

    public static Task<T> ExecuteDelete<T>(
        this HttpClient httpClient,
        string url,
        object data = null,
        IDictionary<string, string> headers = null,
        CancellationToken cancellationToken = default)
    {
        return httpClient.Execute<T>(
            HttpMethod.Delete,
            url,
            data: data,
            headers: headers,
            cancellationToken: cancellationToken);
    }

    public static async Task<T> Execute<T>(
        this HttpClient httpClient,
        HttpMethod method,
        string url,
        object data = null,
        IDictionary<string, string> headers = null,
        CancellationToken cancellationToken = default)
    {
        using var requestMessage = new HttpRequestMessage(method, url);
        HttpContent content = null;

        if (data != null)
        {
            if (data is string value)
            {
                content = new StringContent(value, Encoding.UTF8, "application/json");
            }
            else if (data is FormUrlEncodedContent)
            {
                content = data as FormUrlEncodedContent;
            }
            else
            {
                var json = JsonConvert.SerializeObject(data);

                content = new StringContent(json, Encoding.UTF8, "application/json");
            }

            requestMessage.Content = content;
        }

        if (headers?.Count > 0)
        {
            foreach (var a in headers)
            {
                if (content != null && (a.Key == "Content-Type" || a.Key == "Content-Length"))
                {
                    continue;
                }

                requestMessage.Headers.Add(a.Key, a.Value);
            }
        }

        using var response = await httpClient.SendAsync(requestMessage, cancellationToken).ConfigureAwait(false);
        content?.Dispose();

        if (response.Content != null)
        {
            object responseData = await response.Content.ReadAsStringAsync(cancellationToken).ConfigureAwait(false);
            object resultData;

            if (typeof(T) == typeof(string))
            {
                resultData = responseData != null ? responseData as string : string.Empty;
            }
            else
            {
                resultData = responseData != null
                   ? JsonConvert.DeserializeObject<T>(responseData as string)
                   : GetDefaultValue<T>();
            }

            return (T)resultData;
        }
        else
        {
            throw new ArgumentNullException($"request to {url} error {response.StatusCode}");
        }
    }
    public static T GetDefaultValue<T>()
    {
        return (T)GetDefaultValue(typeof(T));
    }

    public static object GetDefaultValue(this Type type)
    {
        return type.GetTypeInfo().IsValueType ? Activator.CreateInstance(type) : null;
    }
}


