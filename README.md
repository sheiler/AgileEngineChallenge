# AgileEngineChallenge

# TestApp
https://agileenginechallenge.azurewebsites.net/swagger/index.html

# Log

AOO(50 min)
Thursday
21.00 Read for the first time
21.20 Analizing points of challenge
21.25 Testing endpoints
21.30 Enumerating endpoints to work with
21.45 Get objects detalis (from tested enpoints)
22.00

DOO (50 min)
Sunday
14.00 Analizing class model
14.40 Setup repo and solution
14.50 coffee break

POO (130 min)
Sunday
15.00 Start coding
16.00 1st commit
16.15 pause for token error
16.45 solve the token error, sunday break
19.10 Resuming.
20.05 Finish first draft, test final endpoints (first dragt 2:10hs commit)
20.15 Finish fully functional (fix search error commit)

# ToDo List (without time limit)
- Real search algorithm
- Status for cache refresh (and no-lock)
- More advanced caché implementation (maybe redist or some database in azure)
- Unit testing proyect
- Better response model (pagecount, rowcount)
- App config to setup env variables (url, cache reload time, pageSize, apikey)
- Custom exceptions
- Endpoints list in a constant
- T class for cacheservice
- Better approach of repository pattern form ImageService
- Hosted service instead of running fist load of caché in program.cs
- (just a wish) Test Front end

# Detailed Analysis

**********************************************************************************
Endpoints to consume
Base URL: http://interview.agileengine.com
POST
/auth
Body: { "apiKey": "value" }
Response: { "token": "value" }


GET (auth)
/images => ImageModelResponse
/images?page=N => ImageModelResponse
/images/id => ImageDetailsExtended


Endpoints to expose
/images
/images/id
/search/termino => List<ImageDetailsExtended>


Objects to develop
- ImageModelResponse
- ImageDetails
- ImageBase
- AuthRequest
- AuthResponse


RS examples
ImageDetailsExtended
{
    "id": "82245459e53513c81f94",
    "author": "Alienated Summer",
    "camera": "Olympus Tough TG-6",
    "tags": "#beauty #photo #greatview #beautifulday #photooftheday #life #nature #photography #wonderfullife ",
    "cropped_picture": "http://interview.agileengine.com/pictures/cropped/0020.jpg",
    "full_picture": "http://interview.agileengine.com/pictures/full_size/0020.jpg"
}

ImageModelResponse
{
    "pictures": [
        {
            "id": "90322bc9cd20fd0f4ffa",
            "cropped_picture": "http://interview.agileengine.com/pictures/cropped/0002.jpg"
        },
        {
            "id": "d8b093f6ad76844e959d",
            "cropped_picture": "http://interview.agileengine.com/pictures/cropped/0015.jpg"
        }
    ],
    "page": 1,
    "pageCount": 26,
    "hasMore": true
}

AuthRequest
{ "apiKey": "value" }

AuthResponse
{ "token": "value" }

**************************************************************

New class model

- ServiceExtensions
  - Cors
  - Swagger
  - Http
  - Service

- ICacheImage
- MemoryCacheImage : MemoryCache
 -> Add<key, object> : ImageDetails
 -> GetAll : List<ImageDetails>
 -> GetByKey : ImageDetails
 -> GetByMeta : ImageDetails

 
- IImageService
- ImageService
 - _cacheClient
 - _agileClient
 - RefreshCache()
 + GetByPage(int page)
 + GetById(int id)
 + GetByMeta(string meta)

- IAgileEngineClient
- AgileEngineClient
  - _httpClient
  - Get()
  - Post()
  - RefreshToken()
  + GetImages(page = 1) : ImageModelResponse
  + GetImageDetails(id) : ImageDetails

- ImageController
 - Get(int page = 1)
 - GetById(string id)
 - GetByMeta(string meta)

- AppStart
 - Call migration

*****************************************************************

Solution setup

- net core 5, api rest
- 2 layers, Application, Infrastructure
- Libraries: Default HttpClient, Default InMemory Caché, Swagger
- Repo in github (3 branchs master, "2hs" and "limitless")
- Test App service in azure
