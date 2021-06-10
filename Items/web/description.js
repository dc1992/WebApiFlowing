//import { getQueryStringUrl, toReadableDate } from './utils'

writeDescription()

async function writeDescription() {
	
    var output = document.getElementById("dummy")

	let baseUrl = top.baseUrl;
	let userGuid = top.userGuid;

    //fetch data from server
    let estimatedDateForReachingWeightUrl = getQueryStringUrl(`${baseUrl}/EstimatedDateForReachingWeight`, userGuid);
    let estimatedDateForReachingWeightResponse = await fetch(estimatedDateForReachingWeightUrl);
    let estimatedDateForReachingWeightJson = await estimatedDateForReachingWeightResponse.json();
	
	let date = new Date(estimatedDateForReachingWeightJson.estimatedDate)
	output.innerHTML += `Raggiungerai il tuo peso desiderato (${estimatedDateForReachingWeightJson.desiredWeightInKgs} kg) il ${toReadableDate(date)}`
}

function getQueryStringUrl(endPoint, userGuid) {
    
    var url = new URL(endPoint),
        params =  {userGuid: userGuid};
    Object.keys(params).forEach(key => url.searchParams.append(key, params[key]));

    return url;
}

function toReadableDate(date) {

	return date.toISOString().split('T')[0];
}