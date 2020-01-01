"use strict";

// The current application coordinates were pre-registered in a B2C tenant.
var appConfig = {
	b2cScopes: [
		"https://temisadb2cstaging.onmicrosoft.com/apigateway/api.read"
	]
};

// configuration to initialize msal
var msalConfig = {
	auth: {
		clientId: "396444ca-4d0d-482b-a72a-1e9449bae2fa", //This is your client ID
		authority: "https://temisadb2cstaging.b2clogin.com/temisadb2cstaging.onmicrosoft.com/B2C_1_temisstaging_signin", //This is your tenant info
		authorityPR: "https://temisadb2cstaging.b2clogin.com/temisadb2cstaging.onmicrosoft.com/B2C_1_temisstaging_pwd_reset",
		validateAuthority: false
	},
	cache: {
		cacheLocation: "localStorage",
		storeAuthStateInCookie: true
	}
};

// request to signin - returns an idToken
var loginRequest = {
	scopes: appConfig.b2cScopes,
	extraQueryParameters: {
		ui_locales: 'es'
	}
};

// request to acquire a token for resource access
var tokenRequest = {
	scopes: appConfig.b2cScopes
};