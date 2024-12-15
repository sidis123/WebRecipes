import React, { useState, useEffect } from 'react'
import { CRow, CContainer } from '@coreui/react'
import axios from 'axios'
import RecipeCard from '../RecipeCard'
const CategoryWithRecipes = ({ category }) => {
  const [recipes, setRecipes] = useState([])
  const [loading, setLoading] = useState(true)
  const [error, setError] = useState(null)

  // Fetch recipes by category ID
  useEffect(() => {
    console.log('Category:', category)
    if (category) {
      axios
        .get(`https://localhost:7120/api/Category/${category}/receptai`, {
          headers: {
            Authorization: `Bearer ${localStorage.getItem('token')}`,
          },
        })
        .then((response) => {
          setRecipes(response.data.receptai) // Adjust to match the 'receptai' key in the endpoint
          setLoading(false)
        })
        .catch((error) => {
          setError(error)
          setLoading(false)
        })
    }
  }, [category])

  if (loading) {
    return <div>Loading...</div>
  }

  if (error) {
    return <div>Error fetching recipes: {error.message}</div>
  }

  return (
    <CContainer>
      <h2 className="my-4 text-center">
        {category.pavadinimas} Recipes that belong to this category
      </h2>
      <CRow className="g-4">
        {recipes.length > 0 ? (
          recipes.map((recipe, index) => (
            <RecipeCard
              key={index} // Use index if no id is provided
              recipe={recipe}
            />
          ))
        ) : (
          <div className="text-center">No recipes available in this category.</div>
        )}
      </CRow>
    </CContainer>
  )
}

export default CategoryWithRecipes
