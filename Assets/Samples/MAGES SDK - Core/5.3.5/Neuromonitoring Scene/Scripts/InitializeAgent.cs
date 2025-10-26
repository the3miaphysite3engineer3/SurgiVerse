namespace MAGES.NeuromonitoringScene
{
    using MAGES;
#if MAGES_MICROSOFT_COGNITIVE_SERVICES
#if MAGES_CHARACTER_CONTROLLER
    using MAGES.CharacterController;
#endif
    using MAGES.Experimental.EmbodimentJARIA;
#endif
    using UnityEngine;

    /// <summary>
    /// Step to initialize the embodied agent in neuromonitoring sample scene.
    /// </summary>
    public class InitializeAgent : Step
    {
        private GameObject nonitorScreen;
#if MAGES_MICROSOFT_COGNITIVE_SERVICES
        private AgentConfiguration neuroAgent;
#endif

        /// <summary>
        /// Execution of step.
        /// </summary>
        /// <param name="action">The action state.</param>
        /// <param name="stepEvent">The event payload of the step.</param>
        public override void Execute(BaseActionData action, StepEvent stepEvent)
        {
#if MAGES_MICROSOFT_COGNITIVE_SERVICES
            neuroAgent = Hub.Instance.Get<EmbodimentJARIAModule>().InitializeAgent(0);
            nonitorScreen = GameObject.Find("UI");

            var target = Hub.Instance.Rig.HeadCamera.gameObject;
            Hub.Instance.Get<EmbodimentJARIAModule>().UserSubsComponent.GetComponent<LookAtObject>().TargetObject(target.transform);
#if MAGES_CHARACTER_CONTROLLER
            if (MAGESCharacterController.Instances.TryGetValue("Embodiment Pharmacist", out MAGESCharacterController controller))
            {
                MAGESCharacterController.Instances["Embodiment Pharmacist"]?.MAGESHeadController.LookAt(target, 0);
            }
#endif

            neuroAgent.OnStartTalking.AddListener(OnStartTalking);
            neuroAgent.OnEndTalking.AddListener(OnEndTalking);
            neuroAgent.OnInteract.AddListener(Interact);
            neuroAgent.OnCancel.AddListener(Cancel);
#endif
        }

#if MAGES_MICROSOFT_COGNITIVE_SERVICES
        private void OnStartTalking()
        {
#if MAGES_CHARACTER_CONTROLLER
            if (MAGESCharacterController.Instances.TryGetValue("Embodiment Pharmacist", out MAGESCharacterController controller))
            {
                MAGESCharacterController.Instances["Embodiment Pharmacist"].MAGESAnimator.PlayAnimation("Talking");
            }
#endif
        }

        private void OnEndTalking()
        {
#if MAGES_CHARACTER_CONTROLLER
            if (MAGESCharacterController.Instances.TryGetValue("Embodiment Pharmacist", out MAGESCharacterController controller))
            {
                MAGESCharacterController.Instances["Embodiment Pharmacist"]?.MAGESAnimator.StopAllOnPlayAnimations();
            }
#endif
        }

        private void Interact()
        {
            if (neuroAgent.AgentOutput != null)
            {
                neuroAgent.AgentOutput.SetActive(true);
            }

            if (nonitorScreen != null)
            {
                foreach (Transform child in nonitorScreen.transform)
                {
                    child.gameObject.SetActive(false);
                }
            }
        }

        private void Cancel()
        {
            if(neuroAgent.AgentOutput != null)
            {
                neuroAgent.AgentOutput.SetActive(false);
            }

            if (nonitorScreen != null)
            {
                foreach (Transform child in nonitorScreen.transform)
                {
                    child.gameObject.SetActive(true);
                }
            }
#if MAGES_CHARACTER_CONTROLLER
            if (MAGESCharacterController.Instances.TryGetValue("Embodiment Pharmacist", out MAGESCharacterController controller))
            {
                controller.MAGESAnimator.StopAllOnPlayAnimations();
            }
#endif
        }
#endif
        }
    }