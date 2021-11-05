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
    graficoBarra(participacionesPorDia, dias, { element: "chartFecha", title: "Participación según día", axisX: "" })
    graficoBarra(participacionesPorPublico, publicos, { element: "chartPublico", title: "Participación según público", axisX: "" })
    graficoBarra(participacionesPorComplejidad, complejidades, { element: "chartComplejidad", title: "Participación según complejidad", axisX: "" })
})

seleccion.addEventListener('change', function (event) {

    var opcion = event.target.value

    if (opcion == "linea") {
        graficoLinea(participacionesPorDia, dias, { element: "chartFecha", title: "Participación según día", axisX: "" })
        graficoLinea(participacionesPorPublico, publicos, { element: "chartPublico", title: "Participación según público", axisX: "" })
        graficoLinea(participacionesPorComplejidad, complejidades, { element: "chartComplejidad", title: "Participación según complejidad", axisX: "" })

    } else if (opcion == "barra") {
        graficoBarra(participacionesPorDia, dias, { element: "chartFecha", title: "Participación según día", axisX: "Días" })
        graficoBarra(participacionesPorPublico, publicos, { element: "chartPublico", title: "Participación según público", axisX: "Público Dirigido" })
        graficoBarra(participacionesPorComplejidad, complejidades, { element: "chartComplejidad", title: "Participación según complejidad", axisX: "" })
    }
})
