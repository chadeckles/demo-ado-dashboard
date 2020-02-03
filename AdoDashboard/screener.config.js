console.log(process.env);
module.exports = {
    // full repository name for your project:
    projectRepo: 'testVisual',

    // this example assumes Environment Variables listed below exist on your system:
    apiKey: process.env.SCREENER_API_KEY,
    // array of UI states to capture visual snapshots of.
    // each state consists of a url and a name.
    states: [
        {
            url: 'http://www.google.com',
            name: 'Name of UI State'
        }
    ]
};