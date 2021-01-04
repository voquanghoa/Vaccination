Name=$1
if [ $# -eq 0 ]
  then
    Name=Hello
fi

echo Add Migration $Name
dotnet ef migrations add $Name -s AspCoreAPIStarter -p AspDataModel