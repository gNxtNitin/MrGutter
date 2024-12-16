// Start For customerDashboard Charts

//$(window).on("ready", function () {
$(function () {
    var options = {
        series: [{
            name: 'Loan Amount',
            
            data: [1, 5, 2, 9, 5, 11, 5, 10, 2, 15, 9, 2 ],
            //color: "#EE6D7A"
            color: "#1C64AE"
        }, {
            name: 'Outstanding',
            data: [0, 2, 1, 6, 2, 8, 2, 7, 1,10, 6,1],
            //color: "#EE6D7A"
            color: "#EB2B26"
        }],
        chart: {
            height: 350,
            type: 'area'
        },
        dataLabels: {
            enabled: false
        },
        stroke: {
            curve: 'smooth'
        },
        xaxis: {
            type: 'month',
            categories: ["Jan", "Feb", "Mar", "Apr", "May", "Jun", "Jul", "Aug", "Sep", "Oct", "Nov", "Dec"]
        },
      
        yaxis: {
            labels: {

                formatter: function (value) {
                    return value + " Cr";
                }
            }
        },
        //fill: {
        //    colors: ['#1C64AE', '#EB2B26', '#9C27B0'],
        //    opacity:10
        //},
        //fill: {
        //    type: 'gradient',
        //    gradient: {
        //        shade: 'dark',
        //        type: "vertical",
        //        shadeIntensity:0.5,
        //        gradientToColors: undefined, // optional, if not defined - uses the shades of same color in series
        //        inverseColors: true,
        //        opacityFrom: 0.8,
        //        opacityTo: 0.8,
        //        stops: [50, 100],
        //        colorStops: []
        //    }
        //},
        // xaxis: {
        //     labels: {
        //         formatter: function (value) {
        //             return value;
        //         }
        //     }
        // },
        tooltip: {
            enabled: true,
            // x: {
            //     format: 'numeric'
            // }
        }
       
    };

    var customerChart = new ApexCharts(document.querySelector("#chartLine"), options);
    customerChart.render();
   

});

// End For customerDashboard Charts
