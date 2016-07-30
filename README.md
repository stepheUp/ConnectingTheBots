# ConnectingTheBots
DX TR23 Hackathon

Techready FAQ Bot, backuped by a real human being if answer not found in the FAQ

Provides a Skype integration

----------
Technology used:
- Service Fabric
   - Knowledge Base service (stateless) 
   - Assist Service (Worker statefull) + facade (WebAPI stateless)
- Bot Framework
- DocumentDB for Knowledge Base
- Continuous integration via VSTS & Github : Build & Azure deployment are triggered on GitHub checkin [https://stephe.visualstudio.com/TR23%20Hackathon]

Team:
- dominpo (Assist Service)
- belepich (Bot)
- delabarr (Knowledge Base service)
- stephe (devops)
