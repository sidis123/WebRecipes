import React, { useState } from 'react'
import { CCard, CCardBody, CRow, CCol, CFormTextarea, CButton } from '@coreui/react'
import axios from 'axios'

const CreateComment = ({ recipeId, onCommentCreated }) => {
  const [state, setState] = useState({
    tekstas: '',
    patiktukai: 0, // Default likes
  })

  const [isSubmitting, setIsSubmitting] = useState(false) // Loading state
  const token = localStorage.getItem('token')
  const userId = localStorage.getItem('userid')

  const handleChange = (e) => {
    setState({
      ...state,
      [e.target.name]: e.target.value,
    })
  }

  const handleSubmit = () => {
    if (!state.tekstas.trim()) {
      alert('Comment cannot be empty!')
      return
    }

    setIsSubmitting(true) // Start loading
    axios
      .post(
        `https://localhost:7120/api/Comment?userId=${userId}&recipeId=${recipeId}`,
        {
          id_Komentaras: 0,
          patiktukai: 0,
          sukurimo_data: new Date().toISOString(),
          tekstas: state.tekstas,
        },
        {
          headers: {
            Authorization: `Bearer ${token}`,
          },
        },
      )
      .then(() => {
        alert('Comment created successfully!')
        setState({ tekstas: '', patiktukai: 0 }) // Reset input
        if (onCommentCreated) onCommentCreated() // Callback to refresh comments
      })
      .catch((error) => {
        console.error('Error creating comment:', error)
        alert('Failed to create comment.')
      })
      .finally(() => setIsSubmitting(false)) // End loading
  }

  return (
    <CRow className="justify-content-center my-4">
      <CCol xs={12} md={8}>
        <CCard className="shadow-sm">
          <CCardBody>
            <h5 className="mb-3">Add a Comment</h5>
            <CFormTextarea
              rows={3}
              placeholder="Write your comment here..."
              name="tekstas"
              value={state.tekstas}
              onChange={handleChange}
              className="mb-3"
            ></CFormTextarea>
            <div className="text-end">
              <CButton
                color="primary"
                onClick={handleSubmit}
                disabled={isSubmitting || !state.tekstas.trim()}
              >
                {isSubmitting ? 'Submitting...' : 'Comment'}
              </CButton>
            </div>
          </CCardBody>
        </CCard>
      </CCol>
    </CRow>
  )
}

export default CreateComment
