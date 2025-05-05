using System;
using System.Text.Json;
using API.helpers;

namespace API.Extensions;

public static class HttpExtensions
{
    public static void AddPaginationHeader<T>(this HttpResponse response, PagedList<T> data)
    {
        var PaginationHeader = new PaginationHeader
        (
            data.CurrentPage, 
            data.PageSize,
            data.TotalCount, 
            data.TotalPages
        );

        var jsonOptions = new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase };
        response.Headers.Append("Pagination", JsonSerializer.Serialize(PaginationHeader, jsonOptions));
        response.Headers.AccessControlExposeHeaders = "Pagination";
        
    }
}
