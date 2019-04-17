using Builder.Models;
using CSharpVerbalExpressions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Builder
{
    class Program
    {
        static void Main(string[] args)
        {
            FluentRegexTest();

            FluentPhoneTest();

            StrongFluentHtmlVisitBuilderTest();

            FluentVisitBuilderTest();

            VisitBuilderTest();

            // StringBuilderTest();
        }

        private static void FluentRegexTest()
        {
            string url = "http://www.altkom.pl";

            // Install-Package VerbalExpressions-official
            string pattern = @"^(http)(s)?([^\ ]*)$";

            Regex urlRegex = new Regex(pattern);
            urlRegex.IsMatch(url);

            VerbalExpressions expression = new VerbalExpressions()
                .StartOfLine()
                .Then("http")
                .Maybe("s")
                .AnythingBut(" ")
                .EndOfLine();

            bool isValid = expression.Test(url);

            Regex regex = expression.ToRegex();
            isValid = regex.IsMatch(url);
        }

        private static void FluentPhoneTest()
        {
            FluentPhone
                .On
                .From("555-666-777")
                .To("555-888-333")
                .To("555-434-000")
                .WithSubject("Wzorce projektowe w C#")
                .Call();

            FluentPhone
                .On
                .From("555-666-777")
                .To("555-888-333")                
                .Call();

        }

        private static void StrongFluentHtmlVisitBuilderTest()
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

            string report = StrongFluentHtmlVisitBuilder
                .Instance(visit)
                .AddHeader()
                .AddContent()
                .AddFooter()
                .Build();
        }

        private static void FluentVisitBuilderTest()
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


            FluentHtmlVisitBuilder visitBuilder = new FluentHtmlVisitBuilder(visit);

            string report = visitBuilder
                                .AddFooter()
                                .AddHeader()
                                .AddContent()
                                .Build();


            Console.WriteLine(report);
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
            visitBuilder.AddHeader();
            visitBuilder.AddFooter();


            // Fluent Api
            //visitBuilder
            //        .AddHeader()
            //        .AddContent()
            //        .AddFooter()
            //        .Build();

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


    interface IHeader
    {
        IContent AddHeader();
    }

    interface IContent
    {
        IFooter AddContent();
    }

    interface IFooter
    {
        IReport AddFooter();
    }

    interface IReport
    {
        string Build();
    }

    class StrongFluentHtmlVisitBuilder : IHeader, IContent, IFooter, IReport
    {
        private readonly Visit visit;

        // product
        private string report;

        public static IHeader Instance(Visit visit)
        {
            return new StrongFluentHtmlVisitBuilder(visit);
        }

        protected StrongFluentHtmlVisitBuilder(Visit visit)
        {
            this.visit = visit;

            report = "<html>";
        }

        public IContent AddHeader()
        {
            report += $"<title>Wizyta dn. {visit.VisitDate}</title>";

            return this;
        }

        public IFooter AddContent()
        {
            report += $"<b>Czas wizyty: {visit.Duration}</b>";
            report += $"Pacjent: {visit.Patient.FullName}";

            return this;
        }

        public IReport AddFooter()
        {
            report += "<hr>";

            return this;
        }


        public string Build()
        {
            EndReport();

            return report;
        }

        private void EndReport()
        {
            report += "</html>";
        }
    }

    class FluentHtmlVisitBuilder
    {
        private readonly Visit visit;

        // product
        private string report;

        public FluentHtmlVisitBuilder(Visit visit)
        {
            this.visit = visit;

            report = "<html>";
        }

        public FluentHtmlVisitBuilder AddHeader()
        {
            report += $"<title>Wizyta dn. {visit.VisitDate}</title>";

            return this;
        }

        public FluentHtmlVisitBuilder AddContent()
        {
            report += $"<b>Czas wizyty: {visit.Duration}</b>";
            report += $"Pacjent: {visit.Patient.FullName}";

            return this;
        }

        public FluentHtmlVisitBuilder AddFooter()
        {
            report += "<hr>";

            return this;
        }


        public string Build()
        {
            EndReport();

            return report;
        }

        private void EndReport()
        {
            report += "</html>";
        }
    }

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

        public void AddHeader()
        {
            report += $"<title>Wizyta dn. {visit.VisitDate}</title>";
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

        public string Build()
        {
            EndReport();

            return report;
        }

        private void EndReport()
        {
            report += "</html>";
        }
    }
}
