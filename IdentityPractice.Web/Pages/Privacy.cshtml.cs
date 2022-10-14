using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace IdentityPractice.Web.Pages
{
    public class PrivacyModel : PageModel
    {
        private readonly ILogger<PrivacyModel> _logger;

        public class LineData
        {
            public DateTime xValue;
            public double yValue;
        }

        public List<LineData> ChartData = new List<LineData>() 
        { 
            new LineData{xValue=new DateTime(2005,01,01),yValue=13 },
            new LineData{xValue=new DateTime(2006,01,01),yValue=24 },
            new LineData{xValue=new DateTime(2007,01,01),yValue=36 },
            new LineData{xValue=new DateTime(2008,01,01),yValue=44 },
            new LineData{xValue=new DateTime(2009,01,01),yValue=79 },
            new LineData{xValue=new DateTime(2010,01,01),yValue=133 }

        };
        public PrivacyModel(ILogger<PrivacyModel> logger)
        {
            _logger = logger;
        }

        public void OnGet()
        {
        }
    }
}