/**
 * This class is the controller for the main view for the application. It is specified as
 * the "controller" of the Main view class.
 */
Ext.define('aadb2cspa.view.main.MainController', {
	extend: 'Ext.app.ViewController',

	alias: 'controller.main',

	onItemSelected: function (sender, record) {
		Ext.Msg.confirm('Confirm', 'Are you sure?', 'onConfirm', this);
	},

	onConfirm: function (choice) {
		if (choice === 'yes') {
			//
		}
	},

	initialize: function () {
		var me = this;

		InitialAjaxCalls.addToAjaxCalls(function () {
			Ext.Ajax.request({
				url: 'https://localhost:5001/api/values',
				method: 'GET',
				callback: function (options, success, response) {
					if (success) {
						this.getViewModel().setData({
							datos: response.responseText
						});
					} else {
						console.log("Response Ajax Call:", response);
					}
				},
				scope: me
			});
		});

		InitialAjaxCalls.addToAjaxCalls(function () {
			Ext.Ajax.request({
				url: 'https://localhost:5001/api/valores',
				method: 'GET',
				callback: function (options, success, response) {
					if (success) {
						this.getViewModel().setData({
							claims: response.responseText
						});
					} else {
						console.log("Response Ajax Call:", response);
					}
				},
				scope: me
			});
		});
	}
});
