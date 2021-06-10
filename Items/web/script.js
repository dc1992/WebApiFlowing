window.onload = async function () {

    let baseUrl = 'http://localhost:60906';
    let userGuid = '35dacbf7-c939-4112-aa86-bee0792e37d3';

    //fetch data from server
    let estimatedDateUrl = getQueryStringUrl(`${baseUrl}/EstimatedDateForReachingWeight`, userGuid);
    let estimatedDateResponse = await fetch(estimatedDateUrl);
    let estimatedDateJson = await estimatedDateResponse.json();

    let userWeightsUrl = getQueryStringUrl(`${baseUrl}/UserWeights`, userGuid);
    let userWeightsResponse = await fetch(userWeightsUrl);
    let userWeightsJson = await userWeightsResponse.json();

    //calculate the trend points
    let firstUserWeight = userWeightsJson.weightHistories[0];
    let firstTrendPoint = { x: new Date(firstUserWeight.dateOfMeasurement), y: firstUserWeight.weightInKgs };

    let lastTrendPoint = { x: new Date(estimatedDateJson.estimatedDate), y: estimatedDateJson.desiredWeightInKgs };

    //get every point to draw the graph
    let userWeightPoints = [];
    userWeightsJson.weightHistories.forEach(weight => {
        userWeightPoints.push({ x: new Date(weight.dateOfMeasurement), y: weight.weightInKgs })	
    });
    
    //now draw the graph
    renderChart(firstTrendPoint, lastTrendPoint, userWeightPoints);
}

function getQueryStringUrl(endPoint, userGuid) {
    
    var url = new URL(endPoint),
        params =  {userGuid: userGuid};
    Object.keys(params).forEach(key => url.searchParams.append(key, params[key]));

    return url;
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
                { x: new Date(lastTrendPoint.x), y: lastTrendPoint.y, indexLabel: lastTrendPoint.x.toISOString().split('T')[0] }
            ]
        }
    ]
    });

    chart.render();
}