import React from 'react'
import axios from 'axios'
import { useEffect, useState } from 'react'
import { CCard, CCardBody, CCardHeader, CRow, CCol, CAvatar } from '@coreui/react'

const AllRecipeComments = ({ recipeId }) => {
  const token = localStorage.getItem('token')
  const [comments, setComments] = useState([])
  useEffect(() => {
    fetchComments()
  }, [])
  const fetchComments = async () => {
    axios
      .get(`https://localhost:7120/api/Comment`, {
        headers: {
          Authorization: `Bearer ${token}`,
        },
      })
      .then((response) => {
        const filteredComments = response.data.filter(
          (comment) => comment.recipeid_Receptas === recipeId,
        )
        setComments(filteredComments)
      })
      .catch((error) => {
        console.error('Error fetching comments:', error)
      })
  }
  return (
    <CRow className="justify-content-center mt-4">
      <CCol xs={12} md={10}>
        <h2 className="mb-4 text-center">Recipe Comments</h2>
        {comments.length > 0 ? (
          comments.map((comment) => (
            <CCard key={comment.id_Komentaras} className="mb-3 shadow-sm">
              <CCardHeader>
                <strong>Comment ID:</strong> {comment.id_Komentaras} |{' '}
                <small>{new Date(comment.sukurimo_data).toLocaleString()}</small>
              </CCardHeader>
              <CCardBody>
                <p>
                  <strong>Comment:</strong> {comment.tekstas}
                </p>
                <div className="d-flex justify-content-between align-items-center">
                  <div>
                    <span className="text-muted">Likes:</span> {comment.patiktukai}
                  </div>
                  <CAvatar src="https://via.placeholder.com/40" size="md" className="me-2" />
                </div>
              </CCardBody>
            </CCard>
          ))
        ) : (
          <p className="text-center text-muted">No comments available for this recipe.</p>
        )}
      </CCol>
    </CRow>
  )
}
export default AllRecipeComments
