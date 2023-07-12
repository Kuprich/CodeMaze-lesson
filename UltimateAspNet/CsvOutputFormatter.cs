using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.Net.Http.Headers;
using Shared.DataTransferObjects;
using System.Text;

namespace UltimateAspNet;

public class CsvOutputFormatter : TextOutputFormatter
{
    public CsvOutputFormatter()
    {
        SupportedMediaTypes.Add(MediaTypeHeaderValue.Parse("text/csv"));
        SupportedEncodings.Add(Encoding.UTF8);
        SupportedEncodings.Add(Encoding.UTF8);
    }

    protected override bool CanWriteType(Type? type)
    {
        if (typeof(CompanyDto).IsAssignableFrom(type) 
            || typeof(IEnumerable<CompanyDto>).IsAssignableFrom(type))
            return base.CanWriteType(type);
        
        return false;

        
    }

    public override async Task WriteResponseBodyAsync(OutputFormatterWriteContext context, Encoding selectedEncoding)
    {
        var response = context.HttpContext.Response;

        var buffer = new StringBuilder();

        if (context.Object is IEnumerable<CompanyDto> companiesDto)
            foreach (var companyDto in companiesDto)
                FormatCsv(buffer, companyDto);

        if (context.Object is CompanyDto dto)
            FormatCsv(buffer, dto);

        await response.WriteAsync(buffer.ToString());
        
    }

    private static void FormatCsv(StringBuilder buffer, CompanyDto company)
    {
        buffer.AppendLine($"{company.Id},\"{company.Name},\"{company.FullAddress}");
    }
}
