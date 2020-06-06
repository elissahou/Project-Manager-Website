const path = require("path");

module.exports = {
    devtool: 'source-map',
    mode: "development",
    entry: {
        "index": "./source/ts/index.ts",
        "search": "./source/ts/Pages/search.ts",
        "getProjectName": "./source/ts/Pages/getProjectName.ts",
        "categories": "./source/ts/Pages/categories.ts",
        "categoriesAssign": "./source/ts/Pages/categoriesAssign.ts",
        "saveChangesToDescription": "./source/ts/Pages/saveChangesToDescription.ts",
        "deleteInUseProject": "./source/ts/Pages/deleteInUseProject.ts",
        "deleteCategory": "./source/ts/Pages/deleteCategory.ts",
        "renameProjectType": "./source/ts/Pages/renameProjectType.ts",
    },
    output: {
        path: path.resolve(__dirname, "wwwroot", "ts-out"),
        filename: "[name].js",
        publicPath: "/"
    },
    resolve: {
        extensions: [".js", ".ts"]
    },
    module: {
        rules: [
            {
                test: /\.js$/,
                use: ["source-map-loader"],
                enforce: "pre"
            },
            {
                test: /\.ts$/,
                use: "ts-loader"
            }
        ]
    }
};
