const path = require('path');
const copyPlugin = require('copy-webpack-plugin');
const MiniCssExtractPlugin = require('mini-css-extract-plugin');
const RemoveEmptyScriptsPlugin = require('webpack-remove-empty-scripts');

module.exports = {
  module: {
    rules: [
      {
        test: /\.tsx?$/,
        use: 'ts-loader',
        exclude: /node_modules/,
      },
      {
        test: /\.s[ac]ss$/i,
        use: [
          MiniCssExtractPlugin.loader,
          'css-loader',
          {
            loader: "sass-loader",
            options: {
              implementation: require("sass"),
              sourceMap: true,
            },
          },
        ],
      },
    ],
  },

  resolve: {
    extensions: ['.tsx', '.ts', '.js'],
  },

  output: {
    filename: '[name].js',
    sourceMapFilename: '[file].map[query]',
    path: path.resolve(__dirname, '../wwwroot'),
  },

  plugins: [
    new copyPlugin({
      patterns: [
        { from: './node_modules/jquery/dist/jquery.min.js', to: 'lib/jquery.min.js' },
        { from: './node_modules/jquery-validation/dist/jquery.validate.min.js', to: 'lib/jquery.validate.min.js' },
        { from: './node_modules/jquery-validation-unobtrusive/dist/jquery.validate.unobtrusive.min.js', to: 'lib/jquery.validate.unobtrusive.min.js' },
        { from: './node_modules/jquery-validation-unobtrusive-bootstrap/dist/unobtrusive-bootstrap.js', to: 'lib/unobtrusive-bootstrap.js' },
        { from: './node_modules/bootstrap/dist/js/bootstrap.bundle.min.js', to: 'lib/bootstrap.bundle.min.js' },
        { from: './node_modules/bootstrap/dist/js/bootstrap.bundle.min.js.map', to: 'lib/bootstrap.bundle.min.js.map' },
        { 
          from: '**/*',
          context: './src/',
          filter:  async (resourcePath) => {
            const excluded = ['js', 'ts', 'css', 'scss', 'sass', 'map'];
            return !excluded.includes(resourcePath.split('.').pop());
          },
        }
      ],
    }),

    new RemoveEmptyScriptsPlugin(),

    new MiniCssExtractPlugin({
      filename: '[name].css',
    }),
  ],
};
