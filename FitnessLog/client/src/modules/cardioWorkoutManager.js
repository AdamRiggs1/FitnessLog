import { getToken } from "./authManager";

const cardioWorkoutUrl = '/api/CardioWorkout/'


export const getCardioWorkoutbyId = (id) => {
    return getToken().then((token) => {
        return fetch(`${cardioWorkoutUrl}/${id}`, {
            method: "GET",
            headers: {
                Authorization: `Bearer ${token}`,
            },
        }).then((resp) => {
            if (resp.ok) {
                return resp.json();
            } else {
                throw new Error(
                    "An unknown error occurred while trying to get the cardio workout.",
                );
            }
        });
    });
};

export const addCardioWorkout = (cardioWorkout) => {
    return getToken().then((token) => {
        return fetch(cardioWorkoutUrl, {
            method: "POST",
            headers: {
                Authorization: `Bearer ${token}`,
                "Content-Type": "application/json"
            },
            body: JSON.stringify(cardioWorkout)
        }).then((resp) => {
            if (resp.ok) {
                console.log("Workout added successfully!")
                return resp.json();
            } else {
                throw new Error(
                    "An error occurred while trying to add a workout",
                );
            }
        });
    });
}

export const updateCardioWorkout = (cardioWorkout) => {
    return getToken().then((token) => {
        return fetch(`${cardioWorkoutUrl}/${cardioWorkout.id}`, {
            method: "PUT",
            headers: {
                Authorization: `Bearer ${token}`,
                "Content-Type": "application/json"
            },
            body: JSON.stringify(cardioWorkout)
        }).then((resp) => {
            if (resp.ok) {
                console.log("Workout edited successfully!")
                return resp.json();
            } else {
                throw new Error(
                    "An error occurred while trying to edit a workout",
                );
            }
        });
    });
}

export const deleteCardioWorkout = (cardioWorkout) => {
    return getToken().then((token) => {
        return fetch(cardioWorkoutUrl, {
            method: "DELETE",
            headers: {
                Authorization: `Bearer ${token}`,
                "Content-Type": "application/json"
            },
            body: JSON.stringify(cardioWorkout)
        }).then((resp) => {
            if (resp.ok) {
                console.log("Workout deleted successfully!")
                return resp.json();
            } else {
                throw new Error(
                    "An error occurred while trying to delete the workout",
                );
            }
        });
    });
}