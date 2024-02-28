  if (GitHubActions != null)
        {
            Log.Debug("Add Version Postfix if under CI - GithubAction(s)...");

            var buildNumber = GitHubActions.RunNumber;

            if (ScheduledTargets.Contains(Default))
            {
                Version = $"{Version}-ci.{buildNumber}";
            }
            else if (ScheduledTargets.Contains(PrePublish))
            {
                Version = $"{Version}-alpha.{buildNumber}";
            }
        }
//yes
  Target ElectronizeGenericTargetSample => _ => _
        .DependsOn(CompileSample)
        .Executes(() =>
        {
            var sample = SourceDirectory / DemoTargetLibName;
            var cli = SourceDirectory / CliTargetLibName / $"{CliTargetLibName}.csproj";
            var args = "build /target custom win7-x86;win /dotnet-configuration Debug /electron-arch ia32  /electron-params \"--publish never\"";

            DotNet($"run --project {cli} -- {args}", sample);
        });
 Target ElectronizeWindowsTargetSample => _ => _
        .DependsOn(CompileSample)
        .Executes(() =>
        {//hello
            var sample = SourceDirectory / DemoTargetLibName;
            var cli = SourceDirectory / CliTargetLibName / $"{CliTargetLibName}.csproj";
            var args = "build /target win /electron-params \"--publish never\"";

            DotNet($"run --project {cli} -- {args}", sample);
        });
