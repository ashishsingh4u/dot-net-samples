//  ===================================================================================
//  <copyright file="ConfigurationContracts.cs" company="TechieNotes">
//  ===================================================================================
//   TechieNotes Utilities & Best Practices
//   Samples and Guidelines for Winform & ASP.net development
//  ===================================================================================
//   Copyright (c) TechieNotes.  All rights reserved.
//   THIS CODE AND INFORMATION IS PROVIDED "AS IS" WITHOUT WARRANTY
//   OF ANY KIND, EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT
//   LIMITED TO THE IMPLIED WARRANTIES OF MERCHANTABILITY AND
//   FITNESS FOR A PARTICULAR PURPOSE.
//  ===================================================================================
//   The example companies, organizations, products, domain names,
//   e-mail addresses, logos, people, places, and events depicted
//   herein are fictitious.  No association with any real company,
//   organization, product, domain name, email address, logo, person,
//   places, or events is intended or should be inferred.
//  ===================================================================================
//  </copyright>
//  <author>ASHISHSINGH</author>
//  <email>mailto:ashishsingh4u@gmail.com</email>
//  <date>23-12-2012</date>
//  <summary>
//     The ConfigurationContracts.cs file.
//  </summary>
//  ===================================================================================

using System;
using System.Collections.Specialized;
using System.Configuration;
using System.Configuration.Provider;
using System.Web.Configuration;

namespace CustomConfigTest
{
    /// <summary>The page appearance section.</summary>
    public class PageAppearanceSection : ConfigurationSection
    {
        #region Static Fields

        /// <summary>The _lock.</summary>
        private static readonly object _lock = new object();

        /// <summary>The _provider.</summary>
        private static PageAppearanceProvider _provider;

        /// <summary>The _providers.</summary>
        private static PageAppearanceProviderCollection _providers;

        #endregion

        // Create a "color element."
        #region Public Properties

        /// <summary>Gets or sets the color.</summary>
        [ConfigurationProperty("color")]
        public ColorElement Color
        {
            get
            {
                return (ColorElement)this["color"];
            }

            set
            {
                this["color"] = value;
            }
        }

        /// <summary>Gets or sets the default provider.</summary>
        [StringValidator(MinLength = 1)]
        [ConfigurationProperty("defaultProvider", DefaultValue = "PageAppearanceProvider")]
        public string DefaultProvider
        {
            get
            {
                return (string)base["defaultProvider"];
            }

            set
            {
                base["defaultProvider"] = value;
            }
        }

        /// <summary>Gets or sets the example thing elements.</summary>
        [ConfigurationProperty("things")]
        public ExampleThingElementCollection ExampleThingElements
        {
            get
            {
                LoadProviders();
                return (ExampleThingElementCollection)this["things"];
            }

            set
            {
                this["things"] = value;
            }
        }

        /// <summary>Gets or sets the font.</summary>
        [ConfigurationProperty("font")]
        public FontElement Font
        {
            get
            {
                return (FontElement)this["font"];
            }

            set
            {
                this["font"] = value;
            }
        }

        /// <summary>Gets the providers.</summary>
        [ConfigurationProperty("providers")]
        public ProviderSettingsCollection Providers
        {
            get
            {
                return (ProviderSettingsCollection)base["providers"];
            }
        }

        /// <summary>Gets or sets a value indicating whether remote only.</summary>
        [ConfigurationProperty("remoteOnly", DefaultValue = "false", IsRequired = false)]
        public bool RemoteOnly
        {
            get
            {
                return (Boolean)this["remoteOnly"];
            }

            set
            {
                this["remoteOnly"] = value;
            }
        }

        #endregion

        #region Methods

        /// <summary>The load providers.</summary>
        /// <exception cref="ProviderException"></exception>
        private static void LoadProviders()
        {
            // Avoid claiming lock if providers are already loaded
            if (_provider == null)
            {
                lock (_lock)
                {
                    // Do this again to make sure _provider is still null
                    if (_provider == null)
                    {
                        // Get a reference to the <imageService> section
                        var section = (PageAppearanceSection)ConfigurationManager.GetSection("pageAppearanceGroup/pageAppearance");

                        // Load registered providers and point _provider
                        // to the default provider
                        _providers = new PageAppearanceProviderCollection();
                        ProvidersHelper.InstantiateProviders(
                            section.Providers, _providers, typeof(PageAppearanceProvider));
                        _provider = _providers[section.DefaultProvider];

                        if (_provider == null)
                        {
                            throw new ProviderException("Unable to load default ImageProvider");
                        }
                    }
                }
            }
        }

        #endregion

        protected override void DeserializeSection(System.Xml.XmlReader reader)
        {
            base.DeserializeSection(reader);
        }

    }

    // Define the "font" element
    // with "name" and "size" attributes.
    /// <summary>The font element.</summary>
    public class FontElement : ConfigurationElement
    {
        #region Public Properties

        /// <summary>Gets or sets the name.</summary>
        [ConfigurationProperty("name", DefaultValue = "Arial", IsRequired = true)]
        [StringValidator(InvalidCharacters = "~!@#$%^&*()[]{}/;'\"|\\", MinLength = 1, MaxLength = 60)]
        public string Name
        {
            get
            {
                return (String)this["name"];
            }

            set
            {
                this["name"] = value;
            }
        }

        /// <summary>Gets or sets the size.</summary>
        [ConfigurationProperty("size", DefaultValue = "12", IsRequired = false)]
        [IntegerValidator(ExcludeRange = false, MaxValue = 24, MinValue = 6)]
        public int Size
        {
            get
            {
                return (int)this["size"];
            }

            set
            {
                this["size"] = value;
            }
        }

        #endregion
    }

    // Define the "color" element 
    // with "background" and "foreground" attributes.
    /// <summary>The color element.</summary>
    public class ColorElement : ConfigurationElement
    {
        #region Public Properties

        /// <summary>Gets or sets the background.</summary>
        [ConfigurationProperty("background", DefaultValue = "FFFFFF", IsRequired = true)]
        [StringValidator(InvalidCharacters = "~!@#$%^&*()[]{}/;'\"|\\GHIJKLMNOPQRSTUVWXYZ", MinLength = 6, MaxLength = 6
            )]
        public string Background
        {
            get
            {
                return (String)this["background"];
            }

            set
            {
                this["background"] = value;
            }
        }

        /// <summary>Gets or sets the foreground.</summary>
        [ConfigurationProperty("foreground", DefaultValue = "000000", IsRequired = true)]
        [StringValidator(InvalidCharacters = "~!@#$%^&*()[]{}/;'\"|\\GHIJKLMNOPQRSTUVWXYZ", MinLength = 6, MaxLength = 6
            )]
        public string Foreground
        {
            get
            {
                return (String)this["foreground"];
            }

            set
            {
                this["foreground"] = value;
            }
        }

        #endregion
    }

    /// <summary>The thing element.</summary>
    public class ThingElement : ConfigurationElement
    {
        #region Static Fields

        /// <summary>The s_prop color.</summary>
        private static readonly ConfigurationProperty s_propColor;

        /// <summary>The s_prop name.</summary>
        private static readonly ConfigurationProperty s_propName;

        /// <summary>The s_prop type.</summary>
        private static readonly ConfigurationProperty s_propType;

        /// <summary>The s_properties.</summary>
        private static readonly ConfigurationPropertyCollection s_properties;

        #endregion

        #region Constructors and Destructors

        /// <summary>Initializes static members of the <see cref="ThingElement"/> class. 
        ///     Predefines the valid properties and prepares
        ///     the property collection.</summary>
        static ThingElement()
        {
            // Predefine properties here
            s_propName = new ConfigurationProperty(
                "name", typeof(string), null, ConfigurationPropertyOptions.IsRequired);

            s_propType = new ConfigurationProperty("type", typeof(string), "Normal", ConfigurationPropertyOptions.None);

            s_propColor = new ConfigurationProperty("color", typeof(string), "Green", ConfigurationPropertyOptions.None);

            s_properties = new ConfigurationPropertyCollection();

            s_properties.Add(s_propName);
            s_properties.Add(s_propType);
            s_properties.Add(s_propColor);
        }

        #endregion

        #region Public Properties

        /// <summary>
        ///     Gets the Type setting.
        /// </summary>
        [ConfigurationProperty("color")]
        public string Color
        {
            get
            {
                return (string)base[s_propColor];
            }
        }

        /// <summary>
        ///     Gets the Name setting.
        /// </summary>
        [ConfigurationProperty("name", IsRequired = true)]
        public string Name
        {
            get
            {
                return (string)base[s_propName];
            }
        }

        /// <summary>
        ///     Gets the Type setting.
        /// </summary>
        [ConfigurationProperty("type")]
        public string Type
        {
            get
            {
                return (string)base[s_propType];
            }
        }

        #endregion

        #region Properties

        /// <summary>
        ///     Override the Properties collection and return our custom one.
        /// </summary>
        protected override ConfigurationPropertyCollection Properties
        {
            get
            {
                return s_properties;
            }
        }

        #endregion
    }

    /// <summary>The example thing element collection.</summary>
    [ConfigurationCollection(typeof(ThingElement), CollectionType = ConfigurationElementCollectionType.AddRemoveClearMap
        )]
    public class ExampleThingElementCollection : ConfigurationElementCollection
    {
        #region Static Fields

        /// <summary>The m_properties.</summary>
        private static readonly ConfigurationPropertyCollection m_properties;

        #endregion

        #region Constructors and Destructors

        /// <summary>Initializes static members of the <see cref="ExampleThingElementCollection"/> class.</summary>
        static ExampleThingElementCollection()
        {
            m_properties = new ConfigurationPropertyCollection();
        }

        #endregion

        #region Public Properties

        /// <summary>Gets the collection type.</summary>
        public override ConfigurationElementCollectionType CollectionType
        {
            get
            {
                return ConfigurationElementCollectionType.AddRemoveClearMap;
            }
        }

        #endregion

        #region Properties

        /// <summary>Gets the properties.</summary>
        protected override ConfigurationPropertyCollection Properties
        {
            get
            {
                return m_properties;
            }
        }

        #endregion

        #region Public Indexers

        /// <summary>The this.</summary>
        /// <param name="index">The index.</param>
        /// <returns>The <see cref="ThingElement"/>.</returns>
        public ThingElement this[int index]
        {
            get
            {
                return (ThingElement)this.BaseGet(index);
            }

            set
            {
                if (this.BaseGet(index) != null)
                {
                    this.BaseRemoveAt(index);
                }

                this.BaseAdd(index, value);
            }
        }

        /// <summary>The this.</summary>
        /// <param name="name">The name.</param>
        /// <returns>The <see cref="ThingElement"/>.</returns>
        public ThingElement this[string name]
        {
            get
            {
                return (ThingElement)this.BaseGet(name);
            }
        }

        #endregion

        #region Methods

        /// <summary>The create new element.</summary>
        /// <returns>The <see cref="ConfigurationElement"/>.</returns>
        protected override ConfigurationElement CreateNewElement()
        {
            return new ThingElement();
        }

        /// <summary>The get element key.</summary>
        /// <param name="element">The element.</param>
        /// <returns>The <see cref="object"/>.</returns>
        protected override object GetElementKey(ConfigurationElement element)
        {
            return (element as ThingElement).Name;
        }

        #endregion
    }

    /// <summary>The page appearance provider.</summary>
    public class PageAppearanceProvider : ProviderBase
    {
        #region Public Methods and Operators

        /// <summary>The initialize.</summary>
        /// <param name="name">The name.</param>
        /// <param name="config">The config.</param>
        public override void Initialize(string name, NameValueCollection config)
        {
            base.Initialize(name, config);
        }

        #endregion
    }

    /// <summary>The page appearance provider collection.</summary>
    public class PageAppearanceProviderCollection : ProviderCollection
    {
        #region Public Indexers

        /// <summary>The this.</summary>
        /// <param name="name">The name.</param>
        /// <returns>The <see cref="PageAppearanceProvider"/>.</returns>
        public new PageAppearanceProvider this[string name]
        {
            get
            {
                return (PageAppearanceProvider)base[name];
            }
        }

        #endregion

        #region Public Methods and Operators

        /// <summary>The add.</summary>
        /// <param name="provider">The provider.</param>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="ArgumentException"></exception>
        public override void Add(ProviderBase provider)
        {
            if (provider == null)
            {
                throw new ArgumentNullException("provider");
            }

            if (!(provider is PageAppearanceProvider))
            {
                throw new ArgumentException("Invalid provider type", "provider");
            }

            base.Add(provider);
        }

        #endregion
    }
}