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

var seleccion = document.getElementById('opcion')

document.addEventListener('DOMContentLoaded', function () {
    graficoBarra(participacionesPorDia, dias, { element: "chartFecha", title: "Participación según día", axisX: "Días" })
    graficoBarra(participacionesPorPublico, publicos, { element: "chartPublico", title: "Participación según público", axisX: "Público Dirigido" })
    graficoBarra(participacionesPorComplejidad, complejidades, { element: "chartComplejidad", title: "Participación según complejidad", axisX: "Nivél de Complejidad" })
})

seleccion.addEventListener('change', function (event) {

    var opcion = event.target.value

    if (opcion == "linea") {
        graficoLinea(participacionesPorDia, dias, { element: "chartFecha", title: "Participación según día", axisX: "Días" })
        graficoLinea(participacionesPorPublico, publicos, { element: "chartPublico", title: "Participación según público", axisX: "Público Dirigido" })
        graficoLinea(participacionesPorComplejidad, complejidades, { element: "chartComplejidad", title: "Participación según complejidad", axisX: "Nivél de Complejidad" })

    } else if (opcion == "circulo") {

        graficoCirculo(participacionesPorDia, dias, { element: "chartFecha", title: "Participación según día", axisX: "Días" })
        graficoCirculo(participacionesPorPublico, publicos, { element: "chartPublico", title: "Participación según público", axisX: "Público Dirigido" })
        graficoCirculo(participacionesPorComplejidad, complejidades, { element: "chartComplejidad", title: "Participación según complejidad", axisX: "Nivél de Complejidad" })

    } else if (opcion == "barra") {
        graficoBarra(participacionesPorDia, dias, { element: "chartFecha", title: "Participación según día", axisX: "Días" })
        graficoBarra(participacionesPorPublico, publicos, { element: "chartPublico", title: "Participación según público", axisX: "Público Dirigido" })
        graficoBarra(participacionesPorComplejidad, complejidades, { element: "chartComplejidad", title: "Participación según complejidad", axisX: "Nivél de Complejidad" })
    }
})
