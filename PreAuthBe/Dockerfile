# Stage 1: Build - ใช้ .NET SDK Image ที่มีเครื่องมือครบครัน
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY . .
RUN dotnet restore "PreAuthBe.csproj"
RUN dotnet publish "PreAuthBe.csproj" -c Release -o /app/publish

# Stage 2: Final - ใช้ ASP.NET runtime Image ที่มีขนาดเล็กและปลอดภัยกว่า
FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app
COPY --from=build /app/publish .
# ตั้งค่า port ที่จะให้ container ทำงาน
EXPOSE 8080
ENTRYPOINT ["dotnet", "PreAuthBe.dll"]