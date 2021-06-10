writeDescription()

async function writeDescription() {
	
    var output = document.getElementById("dummy")

	let baseUrl = top.baseUrl;
	let userGuid = top.userGuid;

    //fetch data from server
    let estimatedDateForReachingWeightUrl = top.getQueryStringUrl(`${baseUrl}/EstimatedDateForReachingWeight`, userGuid);
    let estimatedDateForReachingWeightResponse = await fetch(estimatedDateForReachingWeightUrl);
    let estimatedDateForReachingWeightJson = await estimatedDateForReachingWeightResponse.json();
	
	let date = new Date(estimatedDateForReachingWeightJson.estimatedDate)
	output.innerHTML += `Raggiungerai il tuo peso desiderato (${estimatedDateForReachingWeightJson.desiredWeightInKgs} kg) il ${top.toReadableDate(date)}`
}