# QuizAPI


#### Description:

Welcome to the Movie Quote Quiz, the challenge for movie buffs and cinephiles! Get ready to put your film knowledge to the test and see if you can identify  some of the most iconic lines from movies spanning several decades.

From the captivating 1970s to the exhilarating 2010s, this quiz takes you on a journey through cinematic history, showcasing memorable quotes from a wide range of movies.


#### Live link:

[Azure Link](https://moviequizfront.azurewebsites.net/)


#### Repo links:

[React Link](https://github.com/bing-me/quotequiz)
[.Net Link](https://github.com/bing-me/QuizAPI)


#### Explanation of code

##### Login page

I will explain each of the sections as one would encounter them in the flow of the user. Here, users will be asked to log in, this is however a 2-in-1 where they can both create an account or log in if their account already exists. It will use the post method from the Participants Controller

POST: api/Participant - This route adds a new participant to the quiz. It receives the participant details in the participant parameter. The route checks if a participant with the same name and email already exists in the Participants table. If not, it adds the participant to the table and saves the changes to the database using SaveChangesAsync. If a participant with the same name and email exists, it assigns the existing participant to the participant variable. Finally, it returns an Ok() result with the added or existing participant.

Note that participants were not scaffolded into the .NET backend as that would create a lot of excess code in terms of logging in, registering, and security. A more minimal approach was chosen as we only need the usernames for a leaderboard.


##### Quiz page

The user will be directed next to the quiz page, here we have the user acknowledge that he will begin the quiz by clicking the button. The button will start a timer that measures the time taken to complete the quiz and call on the get method from the Questions controller.

GET: api/Questions - This route retrieves a list of random questions for the quiz. It selects a random set of five questions from the Questions table in the QuizDbContext. Each question is represented as an anonymous type with properties for QnId (question ID), QnInWords (question in words), ImageName (image name, if applicable), and Options (an array of four options). The selected questions are returned as an Ok() result.


##### Results page

Upon completing the quiz, the participant is now able to see how well they did in the quiz. The GetAnswers method is called from the Questions controller.

POST: api/Questions/GetAnswers - This route retrieves the answers for a given set of question IDs. It receives an array of question IDs (qnIds) in the request. The route queries the Questions table to retrieve the questions that match the provided IDs. For each question, it selects the QnId, QnInWords, ImageName, Options, and Answer. The selected questions with their answers are returned as an Ok() result.

The participant has the option to submit their score, which will update their information in the database with their score. Alternatively, if they are not satisfied with their score, they have the option to retake the quiz.

PUT: api/Participant/5 - This route updates a specific participant's details, such as their quiz score and time taken. It receives the participant's ID and the updated participant details in the participantResult parameter. The route first validates if the ID in the request matches the participant ID in the participantResult. Then it retrieves the current participant details from the Participants table using the Find method, updates the score and time taken, and marks the entity as modified. After saving the changes to the database using SaveChangesAsync, it returns a NoContent() result. If the participant doesn't exist, it returns a NotFound() result.


##### Leaderboard page

All scores can be viewed on the leaderboard which calls the leaderboard method from the Participants controller.

GET: api/leaderboard - This route retrieves a leaderboard of participants ranked by their quiz score and time taken. It queries the Participants table, orders the participants in descending order by score and ascending order by time taken, and selects the participant's name, score, and time taken. The result is returned as an anonymous type list of objects. If no participants are found or the list is empty, it returns a NotFound() result. Otherwise, it returns an Ok() result with the leaderboard data.

