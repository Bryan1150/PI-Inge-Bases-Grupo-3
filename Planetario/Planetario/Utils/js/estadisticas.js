function graficoBarra(datos, labels, graphInfo) {

    var chart = new CanvasJS.Chart(graphInfo.element, {
        animationEnabled: true,
        theme: "light2",
        title: {
            text: graphInfo.title
        },
        axisY: {
            title: "Participantes",
        },
        axisX: {
            title: graphInfo.axisX
        },
        data: [{
            type: "column",
            yValueFormatString: "#,###\"\"",
            dataPoints: datos.map(function (dato, indice) {
                return {
                    label: labels[indice].Text,
                    y: dato
                }
            } )
        }]
    });
    chart.render();
}

function graficoCirculo(datos, labels, graphInfo) {

    var chart = new CanvasJS.Chart(graphInfo.element, {
        animationEnabled: true,
        title: {
            text: graphInfo.title
        },
        data: [{
            type: "pie",
            startAngle: 240,
            yValueFormatString: "#,###\"\"",
            indexLabel: "{label} {y}",
            dataPoints: datos.map(function (dato, indice) {
                return {                
                    y: dato,
                    label: labels[indice].Text
                }
            })
        }]
    });
    chart.render();
}

function graficoLinea(datos, labels, graphInfo) {

    var chart = new CanvasJS.Chart(graphInfo.element, {
        animationEnabled: true,
        theme: "light2",
        title: {
            text: graphInfo.title
        },
        data: [{
            type: "line",
            indexLabelFontSize: 16,
            dataPoints: datos.map(function (dato, indice) {
                return {
                    y: dato,
                    label: labels[indice].Text
                }
            })
        }]
    });
    chart.render();

}
