"use strict";

// instantiate MSAL
var msalInstance = new Msal.UserAgentApplication(msalConfig);

var msalAccessToken = "";
var msalErrorMessage = null;

function getToken() {
	return msalInstance.acquireTokenSilent(tokenRequest)
		// .then(response => {
		// 	// get access token from response
		// 	// response.accessToken
		// 	msalAccessToken = response.accessToken;
		// 	console.log('Ya tiene valor msalAccessToken:', msalAccessToken);
		// })
		.catch(err => {
			// could also check if err instance of InteractionRequiredAuthError if you can import the class.
			if (err.name === "InteractionRequiredAuthError") {
				return msalInstance.acquireTokenRedirect(tokenRequest)
					// .then(response => {
					// 	// get access token from response
					// 	// response.accessToken
					// 	msalAccessToken = response.accessToken;
					// 	console.log('Ya tiene valor msalAccessToken:', msalAccessToken);
					// })
					.catch(err => {
						// handle error
						alert(err);
					});
			}
		});
}

msalInstance.handleRedirectCallback((error, response) => {
	if (error) {
		if (error.errorMessage.indexOf("AADB2C90118") > -1) {
			msalInstance.authority = msalConfig.auth.authorityPR;
			msalInstance.loginRedirect(loginRequest);
			window.stop();
		}
		else if (error.errorMessage.indexOf("Login_In_Progress") > -1) { }
		else if (error.errorMessage.indexOf("AADB2C90118") > -1) {
			window.msalInstance.logout();
		}
	} else {
		if(msalInstance.getAccount().name === undefined) {
			msalInstance.authority = msalConfig.auth.authority;
			msalInstance.loginRedirect(loginRequest);
			window.stop();
		}
	}
});

// if the user is already logged in you can acquire a token
if (!msalInstance.getAccount()) {
	msalInstance.loginRedirect(loginRequest);
}