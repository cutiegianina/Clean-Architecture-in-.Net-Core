import { Injectable } from '@angular/core';
import { fromEvent } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class FormService {

  constructor() { }

  eyeIcon: string = 'fa-solid fa-eye';
  passwordType: string = 'password';

  handleInputLabelAnimation(): void {
    const inputElement = document.querySelectorAll('.form-input');
    inputElement.forEach(element => {
      const inputElementKeyPress = fromEvent(element, 'keyup');
      const elementLabel = document.getElementById(`input-label ${element.id}`)!;

      const getElement = element as HTMLInputElement;

      inputElementKeyPress.subscribe(() => {
        if (getElement.value.length > 0)
          elementLabel.style.display = 'block';
        else
          elementLabel.style.display = 'none';
      });
    });
  }

  toggleEyeIcon(event?: Event): void {
    const iconElement = event?.currentTarget as HTMLElement;
    if (iconElement.classList.contains('fa-eye')) {
      iconElement.className = 'fa-solid fa-eye-slash eye-icon';
      iconElement.setAttribute('type', 'text');
      this.passwordType =  'text';
    } else {
      iconElement.className = 'fa-solid fa-eye eye-icon';
      this.passwordType = 'password';
    }
  }
}
