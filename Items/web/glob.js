top.userGuid = "35dacbf7-c939-4112-aa86-bee0792e37d3"; 

top.baseUrl = 'http://localhost:60906'

top.getQueryStringUrl =  function getQueryStringUrl(endPoint, userGuid) {
    
    var url = new URL(endPoint),
        params =  {userGuid: userGuid};
    Object.keys(params).forEach(key => url.searchParams.append(key, params[key]));

    return url;
}

top.toReadableDate = function toReadableDate(date) {

	return date.toISOString().split('T')[0];
}