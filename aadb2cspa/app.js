var MsalObject = {};
/*
 * This file launches the application by asking Ext JS to create
 * and launch() the Application class.
 */
Ext.application({
    extend: 'aadb2cspa.Application',

    name: 'aadb2cspa',

    requires: [
        // This will automatically load all classes in the aadb2cspa namespace
        // so that application classes do not need to require each other.
        'aadb2cspa.*'
    ],

    // The name of the initial view to create.
    mainView: 'aadb2cspa.view.main.Main'
});
