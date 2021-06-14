writeDescription()

async function writeDescription() {
	
    var idealWeight = document.getElementById("idealWeight")

	let baseUrl = top.baseUrl;
	let userGuid = top.userGuid;

    //fetch data from server
    let idealWeightUrl = top.getQueryStringUrl(`${baseUrl}/IdealWeight`, userGuid);
    let idealWeightResponse = await fetch(idealWeightUrl);
    let idealWeightJson = await idealWeightResponse.json();
	
	idealWeight.innerHTML += `Il tuo peso ideale è fra i ${idealWeightJson.minimumWeightInKgs} kg e i ${idealWeightJson.maximumWeightInKgs} kg`
}