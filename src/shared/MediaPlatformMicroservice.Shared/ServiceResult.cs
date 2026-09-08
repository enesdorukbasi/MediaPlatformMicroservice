using Refit;
using System.Net;
using System.Text.Json;
using System.Text.Json.Serialization;
using ProblemDetails = Microsoft.AspNetCore.Mvc.ProblemDetails;
using ValidationProblemDetails = Microsoft.AspNetCore.Mvc.ValidationProblemDetails;

namespace MediaPlatformMicroservice.Shared;

public class ServiceResult
{
    [JsonIgnore] public HttpStatusCode Status { get; set; }
    public ProblemDetails? Fail { get; set; }

    [JsonIgnore] public bool IsSuccess => Fail is null;
    [JsonIgnore] public bool IsFailure => !IsSuccess;

    public static ServiceResult SuccessAsNoContent()
    {
        return new()
        {
            Status = HttpStatusCode.NoContent
        };
    }
    public static ServiceResult ErrorAsNotFound()
    {
        return new()
        {
            Status = HttpStatusCode.NotFound,
            Fail = new ProblemDetails
            {
                Status = (int)HttpStatusCode.NotFound,
                Title = "Not Found",
                Detail = "The requested resource was not found."
            }
        };
    }
    public static ServiceResult ErrorFromProblemDetails(ApiException exception)
    {
        if (string.IsNullOrEmpty(exception.Content))
        {
            return new()
            {
                Status = exception.StatusCode,
                Fail = new ProblemDetails
                {
                    Status = (int)exception.StatusCode,
                    Title = exception.Message
                }
            };
        }
        var problemDetails = JsonSerializer.Deserialize<ProblemDetails>(exception.Content, new JsonSerializerOptions()
        {
            PropertyNameCaseInsensitive = true,
        });
        return new()
        {
            Status = exception.StatusCode,
            Fail = problemDetails
        };
    }
    public static ServiceResult Error(ProblemDetails problemDetails, HttpStatusCode status)
    {
        return new()
        {
            Status = status,
            Fail = problemDetails
        };
    }
    public static ServiceResult Error(string title, string detail, HttpStatusCode status)
    {
        return new()
        {
            Status = status,
            Fail = new ProblemDetails
            {
                Status = (int)status,
                Title = title,
                Detail = detail
            }
        };
    }
    public static ServiceResult Error(string title, HttpStatusCode status)
    {
        return new()
        {
            Status = status,
            Fail = new ProblemDetails
            {
                Status = (int)status,
                Title = title,
            }
        };
    }
    public static ServiceResult ErrorFromValidation(IDictionary<string, object> errors)
    {
        return new()
        {
            Status = HttpStatusCode.BadRequest,
            Fail = new ValidationProblemDetails()
            {
                Status = (int)HttpStatusCode.BadRequest,
                Title = "Validation Error",
                Detail = "One or more validation errors occurred.",
                Extensions = { { "errors", errors } }
            }
        };
    }
}

public class ServiceResult<T> : ServiceResult
{
    public T? Data { get; set; }
    public string? UrlAsCreated { get; set; }

    public static ServiceResult<T> SuccessAsOk(T data)
    {
        return new()
        {
            Status = HttpStatusCode.OK,
            Data = data
        };
    }
    public static ServiceResult<T> SuccessAsCreated(T data, string url)
    {
        return new()
        {
            Status = HttpStatusCode.Created,
            Data = data,
            UrlAsCreated = url
        };
    }
    public new static ServiceResult<T> ErrorFromProblemDetails(Refit.ApiException exception)
    {
        if (string.IsNullOrEmpty(exception.Content))
        {
            return new()
            {
                Status = exception.StatusCode,
                Fail = new ProblemDetails
                {
                    Status = (int)exception.StatusCode,
                    Title = exception.Message
                }
            };
        }
        var problemDetails = JsonSerializer.Deserialize<ProblemDetails>(exception.Content, new JsonSerializerOptions()
        {
            PropertyNameCaseInsensitive = true,
        });
        return new()
        {
            Status = exception.StatusCode,
            Fail = problemDetails
        };
    }
    public new static ServiceResult<T> Error(ProblemDetails problemDetails, HttpStatusCode status)
    {
        return new()
        {
            Status = status,
            Fail = problemDetails
        };
    }
    public new static ServiceResult<T> Error(string title, string detail, HttpStatusCode status)
    {
        return new()
        {
            Status = status,
            Fail = new ProblemDetails
            {
                Status = (int)status,
                Title = title,
                Detail = detail
            }
        };
    }
    public new static ServiceResult<T> Error(string title, HttpStatusCode status)
    {
        return new()
        {
            Status = status,
            Fail = new ProblemDetails
            {
                Status = (int)status,
                Title = title,
            }
        };
    }
    public new static ServiceResult<T> ErrorFromValidation(IDictionary<string, object> errors)
    {
        return new()
        {
            Status = HttpStatusCode.BadRequest,
            Fail = new ValidationProblemDetails()
            {
                Status = (int)HttpStatusCode.BadRequest,
                Title = "Validation Error",
                Detail = "One or more validation errors occurred.",
                Extensions = { { "errors", errors } }
            }
        };
    }
}