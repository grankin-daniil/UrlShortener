"use strict";

module.exports = {
    entry: "./index.js",
    output: {
        filename: "bundle.js"
    },
    module: {
        loaders: [
            {
                test: /\.jsx?$/,
                loader: "babel-loader",
                exclude: /node_modules/,
                query: {
                    presets: ["es2015", "react"]
                }
            },
            {
                test: /bootstrap\.css/,
                loader: 'css-loader?root=./node_modules/bootstrap/dist/'
            }
        ]
    }
};