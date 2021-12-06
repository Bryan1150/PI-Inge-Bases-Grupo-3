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
                    label: labels[indice]
                }
            })
        }]
    });
    chart.render();
}

document.addEventListener('DOMContentLoaded', function () {
    graficoLinea(cantidad, productos, { element: "chart", title: "Ventas", axisX: "" })
})