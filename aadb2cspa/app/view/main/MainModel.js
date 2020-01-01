/**
 * This class is the view model for the Main view of the application.
 */
Ext.define('aadb2cspa.view.main.MainModel', {
	extend: 'Ext.app.ViewModel',

	alias: 'viewmodel.main',

	data: {
		name: Ext.String.format('<div style="line-height:14px;">{0}<br/><span style="font-size:10px;">{1}</span></div>', window.msalInstance.getAccount().name, window.msalInstance.getAccount().idTokenClaims.emails[0]),

		loremIpsum: 'Lorem ipsum dolor sit amet, consectetur adipisicing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat. Duis aute irure dolor in reprehenderit in voluptate velit esse cillum dolore eu fugiat nulla pariatur. Excepteur sint occaecat cupidatat non proident, sunt in culpa qui officia deserunt mollit anim id est laborum.',

		datos: '',

		claims: ''
	}

	//TODO - add data, formulas and/or methods to support your view
});
