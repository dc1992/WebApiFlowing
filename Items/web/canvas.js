window.onload = async function () {

	try {
		let baseUrl = top.baseUrl;
		let userGuid = top.userGuid;
	
		//fetch data from server
		let firstAndLastTrendPointsUrl = top.getQueryStringUrl(`${baseUrl}/FirstAndLastTrendPoints`, userGuid);
		let firstAndLastTrendPointsResponse = await fetch(firstAndLastTrendPointsUrl);
		let firstAndLastTrendPointsJson = await firstAndLastTrendPointsResponse.json();
	
		let userWeightsUrl = top.getQueryStringUrl(`${baseUrl}/UserWeights`, userGuid);
		let userWeightsResponse = await fetch(userWeightsUrl);
		let userWeightsJson = await userWeightsResponse.json();
	
		//calculate the trend points
		let firstPointFromResponse = firstAndLastTrendPointsJson.firstTrendPoint;
		let firstTrendPoint = { x: new Date(firstPointFromResponse.x), y: firstPointFromResponse.y };
	
		let lastPointFromResponse = firstAndLastTrendPointsJson.lastTrendPoint;
		let lastTrendPoint = { x: new Date(lastPointFromResponse.x), y: lastPointFromResponse.y };
	
		//get every point to draw the graph
		let userWeightPoints = [];
		userWeightsJson.weightHistories.forEach(weight => {
			userWeightPoints.push({ x: new Date(weight.dateOfMeasurement), y: weight.weightInKgs })	
		});
		
		//now draw the graph
		renderChart(firstTrendPoint, lastTrendPoint, userWeightPoints);
	} catch (error) {
		alert("500")
	}
}


function renderChart(firstTrendPoint, lastTrendPoint, userWeightPoints) {

    var chart = new CanvasJS.Chart("chartContainer", {
        animationEnabled: false,
        theme: "light2",
        title:{
            text: "Andamento peso"
        },
        axisX:{
            valueFormatString: "MM YYYY",
            maximum: (new Date(lastTrendPoint.x)).setDate(40),
            crosshair: {
                enabled: true,
                snapToDataPoint: true
            }
        },
        axisY:{
            title: "Peso",
            minimum: lastTrendPoint.y - 2,
            maximum: Math.round(firstTrendPoint.y + 2),
            interval: 2
        },
        data: [{        
            type: "scatter",
              indexLabelFontSize: 16,
            dataPoints: userWeightPoints
        },
        {
            type: "line",
            indexLabelFontSize: 16,
            dataPoints: [
                { x: new Date(firstTrendPoint.x), y: firstTrendPoint.y },
                { x: new Date(lastTrendPoint.x), y: lastTrendPoint.y, indexLabel: top.toReadableDate(lastTrendPoint.x) }
            ]
        }
    ]
    });

    chart.render();
}