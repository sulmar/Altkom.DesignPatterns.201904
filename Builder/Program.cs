using Builder.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Builder
{
    class Program
    {
        static void Main(string[] args)
        {

            VisitBuilderTest();

            // StringBuilderTest();
        }

        private static void VisitBuilderTest()
        {
            Person person = new Person
            {
                FirstName = "John",
                LastName = "Smith",
                Gender = Gender.Male
            };

            Visit visit = new PrivateVisit
            {
                VisitDate = DateTime.Now,
                Duration = TimeSpan.FromHours(2),
                Patient = person
            };

            IVisitBuilder visitBuilder = new HtmlVisitBuilder(visit);

            visitBuilder.AddHeader();
            visitBuilder.AddContent();
            visitBuilder.AddFooter();

            string report = visitBuilder
                .Build();

            Console.WriteLine(report);


        }

        private static void StringBuilderTest()
        {
            //string report = "<b>Hello</b>";

            //report += "<i>World</i>";

            bool isPrivate = true;

            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.AppendLine("<b>Hello</b>");
            stringBuilder.AppendLine("<i>World</i>");

            if (isPrivate)
            {
                stringBuilder.AppendLine("<b>Total amount:</b>");
            }


            string output = stringBuilder.ToString();

            Console.WriteLine(output);


        }
    }

    //abstract class VisitBuilder
    //{
    //    public abstract void AddHeader();
    //    public abstract void AddContent();
    //    public abstract void AddFooter();

    //    public abstract string Build();
    //}


    interface IVisitBuilder
    {
        void AddHeader();
        void AddContent();
        void AddFooter();

        string Build();
    }

    class HtmlVisitBuilder : IVisitBuilder
    {
        private readonly Visit visit;

        // product
        private string report;

        public HtmlVisitBuilder(Visit visit)
        {
            this.visit = visit;

            report = "<html>";
        }

        public void AddContent()
        {
            report += $"<b>Czas wizyty: {visit.Duration}</b>";
            report += $"Pacjent: {visit.Patient.FullName}";
        }

        public void AddFooter()
        {
            report += "<hr>";

        }

        public void AddHeader()
        {
            report += $"<title>Wizyta dn. {visit.VisitDate}</title>";
        }

        public string Build()
        {
            report += "</html>";

            return report;
        }
    }
}
