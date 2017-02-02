'use strict';
var Generator = require('yeoman-generator');
var chalk = require('chalk');
var yosay = require('yosay');

module.exports = Generator.extend({
  prompting: function () {
    this.log(yosay(
      'Welcome to the ' + chalk.red('Kasbah web application') + ' generator!'
    ));

    var prompts = [{
      name: 'namespace',
      message: 'What will be the root namespace of this application?',
      default: 'KasbahWebApplication'
    },
    {
      name: 'alias',
      message: 'What will be the short-name alias application?',
      default: 'kasbahweb'
    }];

    return this.prompt(prompts).then(function (props) {
      // To access props later use this.props.someAnswer;
      this.props = props;
    }.bind(this));
  },

  writing: function () {
    this.fs.copyTpl(this.templatePath('src/Shared/**/*'), this.destinationPath('src/' + this.props.namespace), this.props);
    this.fs.copyTpl(this.templatePath('src/ContentDelivery/**/*'), this.destinationPath('src/' + this.props.namespace + '.ContentDelivery'), this.props);
    this.fs.copyTpl(this.templatePath('src/ContentManagement/**/*'), this.destinationPath('src/' + this.props.namespace + '.ContentManagement'), this.props);
    this.fs.copyTpl(this.templatePath('.vscode/**/*'), this.destinationPath('.vscode/'), this.props);
    this.fs.copy(this.templatePath('global.json'), this.destinationPath('global.json'));
    this.fs.copy(this.templatePath('.gitignore'), this.destinationPath('.gitignore'));
    // TODO: remove this once the package is released on NuGet
    this.fs.copy(this.templatePath('NuGet.config'), this.destinationPath('NuGet.config'));
  },

  install: function () {
    this.spawnCommand('dotnet', ['restore']);
  }
});
