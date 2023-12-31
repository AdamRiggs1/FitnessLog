import { useState, useEffect } from "react";
import { useNavigate, useParams } from "react-router-dom";
import { addFood } from "../modules/foodManager";
import { addUserFood } from "../modules/userFoodManager";
import { me } from "../modules/authManager";


export const AddFoodForm = () => {
    /*
    TODO: Add the correct default properties to the
    initial state object
    */

    //invoke the navigate function
    const navigate = useNavigate()

    //set the food state with the appropriate keys
    const [food, setFood] = useState({
        name: "",
        calories: 0,
        carbohydrates: 0,
        protein: 0,
        fat: 0
    })

    const handleSaveButtonClick = (event) => {
        event.preventDefault()


        // TODO: Create the object to be saved to the API

        //invoke the add food function and put in the food state as the parameter
        return addFood(food)
            //then put in the foodObject as the parameter for the specific user
            .then((foodObject) => {
                me()
                    .then((userProfileObject) => {
                        //make a new state for food from a specifc user
                        const userFood = {
                            userProfileId: userProfileObject.id,
                            foodId: foodObject.id
                        }
                        addUserFood(userFood)

                    })
                    //then navigate back to the food list
                    .then(
                        () => {
                            navigate(`/foodList`)
                        }
                    )
            })
    }


    //make a jsx file to for a form to add a food object
    return (<>
        <form className="addFoodForm">
            <h2 className="addFoodForm__title">Add Food</h2>

            <fieldset>
                <div className="form-group">
                    <label htmlFor="name">Name:</label>
                    <input
                        required autoFocus
                        type="text"
                        className="form-control"
                        placeholder="name of food"
                        value={food.name}
                        onChange={
                            (evt) => {
                                const copy = { ...food }
                                copy.name = evt.target.value
                                setFood(copy)
                            }
                        } />
                </div>
            </fieldset>

            <fieldset>
                <div className="form-group">
                    <label htmlFor="calories"> Calories:</label>
                    <input
                        required autoFocus
                        type="number"
                        className="form-control"
                        placeholder="amount of calories"
                        value={food.calories}
                        onChange={
                            (evt) => {
                                const copy = { ...food }
                                copy.calories = evt.target.value
                                setFood(copy)
                            }
                        } />
                </div>
            </fieldset>

            <fieldset>
                <div className="form-group">
                    <label htmlFor="carbohydrates">Carbohydrates:</label>
                    <input
                        required autoFocus
                        type="number"
                        className="form-control"
                        placeholder="carbohydrates"
                        value={food.carbohydrates}
                        onChange={
                            (evt) => {
                                const copy = { ...food }
                                copy.carbohydrates = evt.target.value
                                setFood(copy)
                            }
                        } />
                </div>
            </fieldset>

            <fieldset>
                <div className="form-group">
                    <label htmlFor="protein">Protein:</label>
                    <input
                        required autoFocus
                        type="number"
                        className="form-control"
                        placeholder="protein"
                        value={food.protein}
                        onChange={
                            (evt) => {
                                const copy = { ...food }
                                copy.protein = evt.target.value
                                setFood(copy)
                            }
                        } />
                </div>
            </fieldset>

            <fieldset>
                <div className="form-group">
                    <label htmlFor="fat">Fat:</label>
                    <input
                        required autoFocus
                        type="number"
                        className="form-control"
                        placeholder="fat"
                        value={food.fat}
                        onChange={
                            (evt) => {
                                const copy = { ...food }
                                copy.fat = evt.target.value
                                setFood(copy)
                            }
                        } />
                </div>
            </fieldset>

            <section className="food_add_buttons">
                <button
                    onClick={(clickEvent) => handleSaveButtonClick(clickEvent)}
                    className="btn btn-primary">
                    Submit Changes
                </button>
                <button className="food_add_back" onClick={() => { navigate("/foodList") }}>Cancel Add Food</button>
            </section>
        </form>
    </>
    )


}

export default AddFoodForm