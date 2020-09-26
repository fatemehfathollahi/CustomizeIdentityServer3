define(["ko", "text!/home/minidashboarComponent", "domReady", "knockout-morris"],
    function (ko, template, domReady, knockoutmorris) {
        function dashboardViewModel() {
            var self = this;
            self.Sale = ko.observable('6,666');
            self.CurrentMonthSale = ko.observable('22%');
            self.View = ko.observable('811');
            self.CurrentMonthView = ko.observable('22%');
            self.User = ko.observable('630');
            self.CurrentMonthUser = ko.observable('60%');
            self.ViewFormAmerica = ko.observable('29%');
            self.ViewFromEurope = ko.observable('68%');
            self.ViewFromAsia = ko.observable('23%');
            line = ko.observable([
                { year: '2008', value: 20 },
                { year: '2009', value: 10 },
                { year: '2010', value: 5 },
                { year: '2011', value: 5 },
                { year: '2012', value: 20 }
            ]);
            donut = ko.observable([
                { label: "Download Sales", value: 12 },
                { label: "In-Store Sales", value: 30 },
                { label: "Mail-Order Sales", value: 20 }
            ]);
            bar = ko.observable([
                { y: '2006', a: 100, b: 90 },
                { y: '2007', a: 75, b: 65 },
                { y: '2008', a: 50, b: 40 },
                { y: '2009', a: 75, b: 65 },
                { y: '2010', a: 50, b: 40 },
                { y: '2011', a: 75, b: 65 },
                { y: '2012', a: 100, b: 90 }
            ]);
            area = ko.observable([
                { y: '2006', a: 100, b: 90 },
                { y: '2007', a: 75, b: 65 },
                { y: '2008', a: 50, b: 40 },
                { y: '2009', a: 75, b: 65 },
                { y: '2010', a: 50, b: 40 },
                { y: '2011', a: 75, b: 65 },
                { y: '2012', a: 100, b: 90 }
            ]);
        }
        return {
            viewModel: dashboardViewModel,
            template: template
        }
    });