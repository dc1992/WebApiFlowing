window.onload = async function () {

    let baseUrl = 'http://localhost:60906';
    let userGuid = '35dacbf7-c939-4112-aa86-bee0792e37d3';
    let weightEndpoint = '/EstimatedDateForReachingWeight';

    let url = getQueryStringUrl(`${baseUrl}${weightEndpoint}`, userGuid);

    let response = await fetch(url, {
        method: 'GET',
        headers: {
            'Content-Type': 'application/json',
            'Access-Control-Allow-Origin': '*'
        }
    });
    

    let json = await response.json();
}

function getQueryStringUrl(endPoint, userGuid) {
    
    var url = new URL(endPoint),
        params =  {userGuid: userGuid};
    Object.keys(params).forEach(key => url.searchParams.append(key, params[key]));

    return url;
}