const path = require('path');
const copyPlugin = require('copy-webpack-plugin');

module.exports = {
  entry: './src/app.ts',
  devtool: 'inline-source-map',
  module: {
    rules: [
      {
        test: /\.tsx?$/,
        use: 'ts-loader',
        exclude: /node_modules/,
      },
    ],
  },
  resolve: {
    extensions: ['.tsx', '.ts', '.js'],
  },
  plugins: [
    new copyPlugin({
      patterns: [
        { from: './node_modules/bootstrap/dist/css/bootstrap.min.css', to: 'css/app.min.css' },
        { from: './node_modules/bootstrap/dist/css/bootstrap.css', to: 'css/app.css' },
        { from: './node_modules/jquery/dist/jquery.min.js', to: 'lib/jquery.min.js' },
        { from: './node_modules/jquery-validation/dist/jquery.validate.min.js', to: 'lib/jquery.validate.min.js' },
        { from: './node_modules/jquery-validation-unobtrusive/dist/jquery.validate.unobtrusive.min.js', to: 'lib/jquery.validate.unobtrusive.min.js' },
        { from: './node_modules/jquery-validation-unobtrusive-bootstrap/dist/unobtrusive-bootstrap.js', to: 'lib/unobtrusive-bootstrap.js' },
        { from: './node_modules/bootstrap/dist/js/bootstrap.bundle.min.js', to: 'lib/bootstrap.bundle.min.js' },
      ],
    }),
  ],
  output: {
    filename: 'js/app.js',
    path: path.resolve(__dirname, '../wwwroot'),
    clean: true,
  },
};
