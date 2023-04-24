using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;

namespace Apparka.WebApi.Web;


public partial class  ResponseDto
{
    public const int OK = 1;
    public const int ERROR = 0;

    public int validacion{get;set;}
    public string mensaje{get;set;}

}