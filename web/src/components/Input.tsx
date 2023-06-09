/* eslint-disable react/display-name */
import React, { InputHTMLAttributes, forwardRef } from 'react'

interface InputProps extends InputHTMLAttributes<HTMLInputElement> {}

export const Input = forwardRef<HTMLInputElement, InputProps>((props, ref) => {
  return (
    <div className="flex h-[56px] w-full items-center rounded-[10px] bg-[#232129] p-[20px]">
      <input
        {...props}
        type="text"
        className="w-full border-none bg-transparent pl-2 outline-none"
        ref={ref}
      />
    </div>
  )
})
