export function getQueryStringUrl(endPoint, userGuid) {
    
    var url = new URL(endPoint),
        params =  {userGuid: userGuid};
    Object.keys(params).forEach(key => url.searchParams.append(key, params[key]));

    return url;
}

export function toReadableDate(date) {

	return date.toISOString().split('T')[0];
}